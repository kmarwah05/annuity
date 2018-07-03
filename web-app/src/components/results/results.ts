import { Chart } from 'chart.js';
import { Data } from 'scripts/data';
import { bindable, inject } from 'aurelia-framework';
import { EventAggregator } from 'aurelia-event-aggregator';

@inject(EventAggregator)
export class Results {
  chartMax: number = 100000;
  
  @bindable data: Data;

  ea: EventAggregator;

  constructor(EventAggregator) {
    this.ea = EventAggregator;
  }

  newInputs() {
    this.ea.publish("new inputs");
  }

  attached() {
    // this.chartMax = this.data.brokerage[0].reduce((a, b) => a > b ? a : b);

    this.buildChart("Brokerage Account",
      "brokerage-account-chart",
      this.data.brokerage,
      true);
    this.buildChart("Variable Annuity",
      "variable-annuity-chart",
      this.data.variable,
      false);
    this.buildChart("Fixed Indexed Annuity",
      "fixed-indexed-annuity-chart",
      this.data.fixedIndexed,
      false);
  }

  buildChart(title: string,
    target: string,
    trials: number[][],
    isFirst: boolean) {
    let ctx = (document.getElementById(target) as HTMLCanvasElement).getContext("2d");
    
    let data = {
      labels: trials[0].map((_, index) => String(index)),
      datasets: []
    };

    trials.forEach(trial => {
      data.datasets.push({
        data: trial,
        fill: false,
        borderColor: "rgba(0, 150, 0, 0.15)",
        pointRadius: 0,
        pointHitRadius: 0,
        pointHoverRadius: 0
      })
    }
    )

    let options = {
      layout: {
        padding: 0
      },
      legend: {
        display: false
      },
      title: {
        display: true,
        text: title,
        fontColor: "rgb(34, 34, 34)",
        fontFamily: "'PT Sans', 'Helvetica Neue', Helvetica, sans-serif",
        fontSize: 28
      },
      events: [],
      elements: {
        line: {
          tension: 0
        }
      },
      animation: {
        duration: 0
      },
      responsiveAnimationDuration: 0,
      scales: {
        xAxes: [{
          ticks: {
            display: false
          }
        }],
        yAxes: [{
          scaleLabel: {
            display: isFirst,
            labelString: "Yearly payout (USD)",
            fontColor: "rgb(34, 34, 34)",
            fontFamily: "'PT Serif', 'Times New Roman', Times, serif",
            fontSize: 18
          },
          ticks: {
            display: isFirst,
            max: this.chartMax,
            min: 0,
            fontColor: "rgb(34, 34, 34)",
            fontFamily: "'PT Serif', 'Times New Roman', Times, serif",
            fontSize: 16
          }
        }]
      }
    };
    
    new Chart(ctx, {
      type: "line",
      data: data,
      options: options
    });
  }
}
