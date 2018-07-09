import { Data } from 'scripts/data';
import { bindable, inject } from 'aurelia-framework';
import { EventAggregator } from 'aurelia-event-aggregator';


// Defined here instead of the end so you don't have to scroll through the valley of the shadow of Chart.js
Number.prototype["format"] = function(n, x) {
  var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\.' : '$') + ')';
  return this.toFixed(Math.max(0, ~~n)).replace(new RegExp(re, 'g'), '$&,');
};

@inject(EventAggregator)
export class Results {
  @bindable data: Data;

  ea: EventAggregator;
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

  newInputs() {
    this.ea.publish("new inputs");
  }
}
