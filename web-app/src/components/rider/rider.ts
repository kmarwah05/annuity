import { bindable, inject } from "aurelia-framework";
import { EventAggregator } from "aurelia-event-aggregator";

@inject(EventAggregator)
export class Rider {
  @bindable rider: string;
  @bindable riders: string[];

  ea: EventAggregator;

  constructor(EventAggregator) {
    this.ea = EventAggregator;
  }

  attached() {
    if (this.riders.includes(this.rider)) {
      document.getElementById(this.rider).classList.add("riders__checkbox--checked");
    }
  }

  toggleRider() {
    document.getElementById(this.rider).classList.toggle("riders__checkbox--checked");

    if (this.riders.includes(this.rider)) {
      this.riders = this.riders.filter(entry => entry != this.rider);
    } else {
      this.riders.push(this.rider);
    }

    this.ea.publish("reload");
  }
}
