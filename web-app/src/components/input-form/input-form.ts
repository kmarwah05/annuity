import { inject, bindable } from "aurelia-framework";
import { EventAggregator } from 'aurelia-event-aggregator';
import { TaxType, FilingStatus, Inputs } from "scripts/inputs";

@inject(EventAggregator)
export class InputForm {
  // Bring types into VM scope
  private FilingStatus: typeof FilingStatus = FilingStatus;
  private TaxType: typeof TaxType = TaxType;

  @bindable inputs: Inputs;

  ea: EventAggregator;

  constructor(EventAggregator) {
    this.ea = EventAggregator;
  }

  clickedSubmit() {
    this.ea.publish("submit");
  }

  selected() {
    this.inputs.isDeferred = !this.inputs.isDeferred;
    let immediate = document.getElementById("immediate");
    let deferred = document.getElementById("deferred");
    let label = document.getElementById("looking-for");
    if (this.inputs.isDeferred) {
      label.innerHTML = "I'm looking for a";
      deferred.classList.add("form__tab-bar--selected");
      immediate.classList.remove("form__tab-bar--selected");
    }
    else {
      label.innerHTML = "I'm looking for an";
      immediate.classList.add("form__tab-bar--selected");
      deferred.classList.remove("form__tab-bar--selected");
    }
  }
}
