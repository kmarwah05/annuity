import { Inputs } from "scripts/inputs";
import { Validator } from "scripts/validator";
import { EventAggregator, Subscription } from "aurelia-event-aggregator";
import { inject } from "aurelia-framework";

@inject(EventAggregator)
export class App {
  private Pages: typeof Pages = Pages;

  inputs: Inputs = new Inputs();
  currentPage: Pages = Pages.Input;

  ea: EventAggregator;
  onSubmit: Subscription;
  onNewInputs: Subscription;
  onReloadWithInputs: Subscription;

  constructor(EventAggregator) {
    this.ea = EventAggregator;
  }

  attached() {
    this.onSubmit = this.ea.subscribe("submit", () => {
      if (Validator.areValidInputs(this.inputs)) {
        this.currentPage = Pages.Results;
      }
    })

    this.onNewInputs = this.ea.subscribe("new inputs", () => {
      this.currentPage = Pages.Input;
    });

    this.onReloadWithInputs = this.ea.subscribe("reload", () => {
      this.currentPage = Pages.Input;
    });
  }
}

enum Pages {
  Input = 0,
  Results
}
