import { Chart } from 'chart.js';
import { Data } from 'scripts/data';
import { bindable, inject } from 'aurelia-framework';
import { EventAggregator } from 'aurelia-event-aggregator';
import { Inputs } from 'scripts/inputs';

@inject(EventAggregator)
export class Results {
  chartMax: number = 15000;
  
  @bindable inputs: Inputs;
  @bindable data: Data;

  ea: EventAggregator;

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

  newInputs() {
    this.ea.publish("new inputs");
  }

  buildChart(title: string,
    target: string,
    trials: number[][],
    isFirst: boolean) {
    let ctx = (document.getElementById(target) as HTMLCanvasElement).getContext("2d");
    
    let bars = trials.map(trial => trial.reduce((a, b) => a + b) / trial.length);
      console.log(bars);
    let data = {
      labels: bars.map((_, index) => String(index)),
      datasets: [{
        label: "Trials",
        data: bars,
        backgroundColor: "#2ecc71",
        borderWidth: 0
      }]
    };

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
      animation: {
        duration: 0
      },
      responsiveAnimationDuration: 0,
      scales: {
        xAxes: [{
          gridLines: {
            display: false
          },
          ticks: {
            display: false
          },
          barPercentage: 1.0,
          categoryPercentage: 1.0
        }],
        yAxes: [{
          scaleLabel: {
            display: isFirst,
            max: this.chartMax,
            min: 0,
            labelString: "Average yearly payout (USD)",
            fontColor: "rgb(34, 34, 34)",
            fontFamily: "'PT Serif', 'Times New Roman', Times, serif",
            fontSize: 18
          },
          ticks: {
            display: isFirst,
            fontColor: "rgb(34, 34, 34)",
            fontFamily: "'PT Serif', 'Times New Roman', Times, serif",
            fontSize: 16
          }
        }]
      }
    };
    
    new Chart(ctx, {
      type: "bar",
      data: data,
      options: options
    });
  }
}

Number.prototype["format"] = function(n, x) {
  var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\.' : '$') + ')';
  return this.toFixed(Math.max(0, ~~n)).replace(new RegExp(re, 'g'), '$&,');
};
