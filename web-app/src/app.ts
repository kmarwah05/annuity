import { Inputs } from "scripts/inputs";
import { Validator } from "scripts/validator";
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

  ea: EventAggregator;
  onSubmit: Subscription;
  onNewInputs: Subscription;
  onReloadWithRiders: Subscription;

  constructor(EventAggregator) {
    this.ea = EventAggregator;
  }

  attached() {
    this.onSubmit = this.ea.subscribe("submit", () => {
      if (Validator.areValidInputs(this.inputs)) {
        APIRequest.postInputs(this.inputs)
        .then(() => {
          this.data = APIRequest.response;
          console.log(this.data);
          this.currentPage = Pages.Results;
        });
      }
    });

    this.onNewInputs = this.ea.subscribe("new inputs", () => {
      this.currentPage = Pages.Input;
    });

    this.onReloadWithRiders = this.ea.subscribe("reload", () => {
      this.currentPage = Pages.Input;
    });
  }
}

enum Pages {
  Input = 0,
  Results
}
