import { Chart } from 'chart.js';
import { Inputs } from 'scripts/inputs';
import { Data } from 'scripts/data';
import { bindable, Container } from 'aurelia-framework';
import { App } from 'app';

export class Results {
  chartMax: number = 10;
  
  response: Data;

  @bindable inputs: Inputs;

  attached() {
    this.buildChart("Brokerage Account",
      "brokerage-account-chart",
      [[2, 3, 4, 1], [4, 5, 8, 3]],
      true);
    this.buildChart("Variable Annuity",
      "variable-annuity-chart",
      [[2, 3, 4, 2], [4, 5, 8, 7]],
      false);
    this.buildChart("Fixed Indexed Annuity",
      "fixed-indexed-annuity-chart",
      [[2, 3, 4, 6], [4, 6, 6, 8]],
      false);
  }

  newInputs() {
    ((Container.instance as any).viewModel as App).onNewInputs();
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
        borderColor: "rgba(0, 150, 0, 0.5)",
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
