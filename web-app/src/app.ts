import { Inputs } from "scripts/inputs";
import { EventAggregator, Subscription } from "aurelia-event-aggregator";
import { inject } from "aurelia-framework";
import { Data } from "scripts/data";
import { APIRequest } from "scripts/api-request";

@inject(EventAggregator)
export class App {
  private Pages: typeof Pages = Pages;

  inputs: Inputs = new Inputs();
  currentPage: Pages = Pages.Input;
  data: Data = new Data();

  isFixed: boolean;
  endAge: number;

  ea: EventAggregator;

  constructor(EventAggregator) {
    this.ea = EventAggregator;
  }

  attached() {
    this.ea.subscribe("send", () => {
      this.inputs.complete(this.isFixed, this.endAge);
      
      APIRequest.postInputs(this.inputs)
      .catch(error => {
        console.log(error);
        APIRequest.response = null;
        this.ea.publish("invalid inputs");
      })
      .then(() => {
        if (APIRequest.response != null) {
          this.data = APIRequest.response;
          this.currentPage = Pages.Results;
        }
      });
    });

    this.ea.subscribe("new inputs", () => {
      this.currentPage = Pages.Input;
    });

    this.ea.subscribe("reload", r => {
      if (r.withRiders) {
        this.inputs.riders = r.withRiders;
        APIRequest.postInputs(this.inputs)
        .then(() => {
          this.data = APIRequest.response;
          this.currentPage = Pages.Results;
        })
        .then(() => {
          this.ea.publish("reload", {
            message: "reloaded",
            data: this.data
          });
        });
      }
    });
  }
}

enum Pages {
  Input = 0,
  Results
}
