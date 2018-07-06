import { Chart } from 'chart.js';
import { Data } from 'scripts/data';
import { bindable, inject } from 'aurelia-framework';
import { EventAggregator } from 'aurelia-event-aggregator';

@inject(EventAggregator)
export class Results {
  @bindable data: Data;

  ea: EventAggregator;
  chart: Chart;
  chartMax: number;
  riders: string[] = [];

  get fixedPercentage() {
    return Math.round(this.data.brokerageBelowFixed * 100);
  }

  get variablePercentage() {
    return Math.round(this.data.variableAboveBrokerage * 100);
  }

  get fixedIndexedPercentage() {
    return Math.round(this.data.fixedIndexAboveBrokerage * 100);
  }

  constructor(EventAggregator) {
    this.ea = EventAggregator;
  }

  attached() {
    this.ea.subscribe("reload", r => {
      if (r === "reloaded") {
        this.buildChart();
        this.buildCharts();
      }
    });

    this.buildChart();
    this.buildCharts();
  }

  newInputs() {
    this.ea.publish("new inputs");
  }

  buildChart() {
    if (this.chart) {
      this.chart.destroy();
    }

    let ctx = (document.getElementById("chart") as HTMLCanvasElement).getContext("2d");
    this.chartMax = Math.ceil(Math.max(this.data.fixedIndexedUpperQuartile) * 2.5 / 10000) * 10000;

    let data = {
      labels: ["Lower Quartile", "Median", "Upper Quartile"],
      datasets: [{
        label: "Brokerage",
        data: [this.data.brokerageLowerQuartile, this.data.brokerageMedian, this.data.brokerageUpperQuartile],
        backgroundColor: "rgb(148, 124, 176)",
        borderWidth: 0
      },
      {
        label: "Variable",
        data: [this.data.variableLowerQuartile, this.data.variableMedian, this.data.variableUpperQuartile],
        backgroundColor: "rgb(89, 171, 227)",
        borderWidth: 0
      },
      {
        label: "Fixed Indexed",
        data: [this.data.fixedIndexedLowerQuartile, this.data.fixedIndexedMedian, this.data.fixedIndexedUpperQuartile],
        backgroundColor: "rgb(4, 147, 114)",
        borderWidth: 0
      },
      {
        label: "Fixed",
        data: [this.data.fixed, this.data.fixed, this.data.fixed],
        backgroundColor: "rgb(135, 211, 124)",
        borderWidth: 0
      }]
    };

    let options = {
      layout: {
        padding: 0
      },
      legend: {
        position: "bottom",
        labels: {
          fontColor: "rgb(34, 34, 34)",
          fontFamily: "'PT Serif', 'Times New Roman', Times, serif",
          fontSize: 18,
          padding: 20
        }
      },
      tooltips: {
        titleFontFamily: "'PT Sans', 'Helvetica Neue', Helvetica, sans-serif",
        titleFontSize: 18,
        bodyFontFamily: "'PT Serif', 'Times New Roman', Times, serif",
        bodyFontSize: 18,
        displayColors: false,
        position: "average",
        callbacks: {
          label: function(tooltipItem, data) {
              let label = data.datasets[tooltipItem.datasetIndex].label || '';

              if (label) {
                  label += ': ';
              }
              label += "$" + tooltipItem.yLabel.format(2, 3);
              return label;
          }
        }
      },
      title: {
        display: true,
        text: "Average yearly payout (USD)",
        fontColor: "rgb(34, 34, 34)",
        fontFamily: "'PT Sans', 'Helvetica Neue', Helvetica, sans-serif",
        fontSize: 30,
        padding: 30
      },
      maintainAspectRatio: true,
      scales: {
        xAxes: [{
          gridLines: {
            display: false
          },
          ticks: {
            display: true,
            fontColor: "rgb(34, 34, 34)",
            fontFamily: "'PT Serif', 'Times New Roman', Times, serif",
            fontSize: 18,
            padding: 10
          },
          barPercentage: 1.0,
          categoryPercentage: 0.7
        }],
        yAxes: [{
          scaleLabel: {
            fontColor: "rgb(34, 34, 34)",
            fontFamily: "'PT Serif', 'Times New Roman', Times, serif",
            fontSize: 18
          },
          ticks: {
            display: true,
            max: this.chartMax,
            fontColor: "rgb(34, 34, 34)",
            fontFamily: "'PT Serif', 'Times New Roman', Times, serif",
            fontSize: 16,
            callback: function(value) {
              return '$' + value.format(2, 3);
            }
          }
        }]
      }
    };
    
    this.chart = new Chart(ctx, {
      type: "bar",
      data: data,
      options: options
    });
  }

  buildCharts() {
    let lowerCtx = (document.getElementById("lower-q-chart") as HTMLCanvasElement).getContext("2d");
    let medianCtx = (document.getElementById("median-chart") as HTMLCanvasElement).getContext("2d");
    let upperCtx = (document.getElementById("upper-q-chart") as HTMLCanvasElement).getContext("2d");

    let lowerData = {
      labels: ["Lower Quartile"],
      datasets: [{
        label: "Brokerage",
        data: [this.data.brokerageLowerQuartile],
        backgroundColor: "rgb(148, 124, 176)",
        borderWidth: 0
      },
      {
        label: "Variable",
        data: [this.data.variableLowerQuartile],
        backgroundColor: "rgb(89, 171, 227)",
        borderWidth: 0
      },
      {
        label: "Fixed Indexed",
        data: [this.data.fixedIndexedLowerQuartile],
        backgroundColor: "rgb(4, 147, 114)",
        borderWidth: 0
      },
      {
        label: "Fixed",
        data: [this.data.fixed],
        backgroundColor: "rgb(135, 211, 124)",
        borderWidth: 0
      }]
    };

    let medianData = {
      labels: ["Median"],
      datasets: [{
        label: "Brokerage",
        data: [this.data.brokerageMedian],
        backgroundColor: "rgb(148, 124, 176)",
        borderWidth: 0
      },
      {
        label: "Variable",
        data: [this.data.variableMedian],
        backgroundColor: "rgb(89, 171, 227)",
        borderWidth: 0
      },
      {
        label: "Fixed Indexed",
        data: [this.data.fixedIndexedMedian],
        backgroundColor: "rgb(4, 147, 114)",
        borderWidth: 0
      },
      {
        label: "Fixed",
        data: [this.data.fixed],
        backgroundColor: "rgb(135, 211, 124)",
        borderWidth: 0
      }]
    };

    let upperData = {
      labels: ["Upper Quartile"],
      datasets: [{
        label: "Brokerage",
        data: [this.data.brokerageUpperQuartile],
        backgroundColor: "rgb(148, 124, 176)",
        borderWidth: 0
      },
      {
        label: "Variable",
        data: [this.data.variableUpperQuartile],
        backgroundColor: "rgb(89, 171, 227)",
        borderWidth: 0
      },
      {
        label: "Fixed Indexed",
        data: [this.data.fixedIndexedUpperQuartile],
        backgroundColor: "rgb(4, 147, 114)",
        borderWidth: 0
      },
      {
        label: "Fixed",
        data: [this.data.fixed],
        backgroundColor: "rgb(135, 211, 124)",
        borderWidth: 0
      }]
    };

    new Chart(lowerCtx, {
      type: "bar",
      data: lowerData,
      options: this.getMobileChartOptions(true, false)
    });

    new Chart(medianCtx, {
      type: "bar",
      data: medianData,
      options: this.getMobileChartOptions(false, false)
    });

    new Chart(upperCtx, {
      type: "bar",
      data: upperData,
      options: this.getMobileChartOptions(false, true)
    });
  }

  getMobileChartOptions(isFirst: boolean, isLast: boolean) {
    return {
      layout: {
        padding: 0
      },
      legend: {
        display: isLast,
        position: "bottom",
        labels: {
          fontColor: "rgb(34, 34, 34)",
          fontFamily: "'PT Serif', 'Times New Roman', Times, serif",
          fontSize: 18,
          padding: 20
        }
      },
      tooltips: {
        titleFontFamily: "'PT Sans', 'Helvetica Neue', Helvetica, sans-serif",
        titleFontSize: 18,
        bodyFontFamily: "'PT Serif', 'Times New Roman', Times, serif",
        bodyFontSize: 18,
        displayColors: false,
        position: "average",
        callbacks: {
          label: function(tooltipItem, data) {
              let label = data.datasets[tooltipItem.datasetIndex].label || '';

              if (label) {
                  label += ': ';
              }
              label += "$" + tooltipItem.yLabel.format(2, 3);
              return label;
          }
        }
      },
      title: {
        display: isFirst,
        text: "Average yearly payout (USD)",
        fontColor: "rgb(34, 34, 34)",
        fontFamily: "'PT Sans', 'Helvetica Neue', Helvetica, sans-serif",
        fontSize: 20,
        padding: 30
      },
      maintainAspectRatio: true,
      scales: {
        xAxes: [{
          gridLines: {
            display: false
          },
          ticks: {
            display: true,
            fontColor: "rgb(34, 34, 34)",
            fontFamily: "'PT Serif', 'Times New Roman', Times, serif",
            fontSize: 16,
            padding: 10
          },
          barPercentage: 1.0,
          categoryPercentage: 0.7
        }],
        yAxes: [{
          scaleLabel: {
            fontColor: "rgb(34, 34, 34)",
            fontFamily: "'PT Serif', 'Times New Roman', Times, serif",
            fontSize: 16
          },
          ticks: {
            display: true,
            max: this.chartMax,
            fontColor: "rgb(34, 34, 34)",
            fontFamily: "'PT Serif', 'Times New Roman', Times, serif",
            fontSize: 16,
            callback: function(value) {
              return '$' + value.format(2, 3);
            }
          }
        }]
      }
    };
  }
}

Number.prototype["format"] = function(n, x) {
  var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\.' : '$') + ')';
  return this.toFixed(Math.max(0, ~~n)).replace(new RegExp(re, 'g'), '$&,');
};
