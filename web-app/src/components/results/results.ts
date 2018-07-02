import { Chart } from 'chart.js';
import { Inputs } from 'scripts/inputs';
import { Data } from 'scripts/data';

export class Results {
  response: Data;
  inputs: Inputs;

  attached() {
    this.buildChart("Variable",
      "variable-annuity-chart",
      [[2, 3, 4, 5], [4, 5, 6, 8]],
      true);
    this.buildChart("Fixed Indexed",
      "fixed-indexed-annuity-chart",
      [[2, 3, 4, 5], [4, 5, 6, 8]],
      false);
    this.buildChart("Brokerage",
      "brokerage-account-chart",
      [[2, 3, 4, 5], [4, 5, 6, 8]],
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
        borderColor: "rgba(0, 255, 0, 0.05)",
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
