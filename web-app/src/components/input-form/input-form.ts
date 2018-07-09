import { inject, bindable, NewInstance } from "aurelia-framework";
import { EventAggregator } from 'aurelia-event-aggregator';
import { ValidationController, validateTrigger, ValidationRules } from 'aurelia-validation';
import { TaxStatus, FilingStatus, Inputs } from "scripts/inputs";
import { isNullOrUndefined } from "util";

@inject(EventAggregator, NewInstance.of(ValidationController))
export class InputForm {
  // Bring types into VM scope
  private FilingStatus: typeof FilingStatus = FilingStatus;
  private TaxStatus: typeof TaxStatus = TaxStatus;

  @bindable inputs: Inputs;
  @bindable isFixed: boolean;
  @bindable endAge: number;

  ea: EventAggregator;
  vController: ValidationController = null;

  constructor(EventAggregator, controller: ValidationController) {
    this.ea = EventAggregator;
    this.vController = controller;

    this.vController.validateTrigger = validateTrigger.manual;
  }

  bind() {
    ValidationRules
      .ensure("amount")
        .required()
        .satisfiesRule("currencyRange", 0, 1000000000)
      .ensure("taxType")
        .satisfies((value, inputs: Inputs) => !(!inputs.isDeferred && isNullOrUndefined(value)))
      .ensure("retireAge")
        .satisfies((value, inputs: Inputs) => !(inputs.isDeferred && isNullOrUndefined(value)))
        .satisfiesRule("integerRange", 0, 119)
      .ensure("isMale")
        .required()
      .ensure("currentAge")
        .required()
        .satisfiesRule("integerRange", 0, 118)
      .ensure("filingStatus")
        .required()
      .ensure("income")
        .required()
        .satisfiesRule("currencyRange", 0, 1000000000)
      .on(this.inputs);
  }

  clickedSubmit() {
    this.vController.validate()
    .then(result => {
      if (result.valid) {
        this.ea.publish("send");
      } else {
        console.log(this.vController.errors);
      }
    });
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

ValidationRules
  .customRule(
    'integerRange',
    (value, _, min, max) => {
      let num = Number.parseInt(value);
      return num === null || num === undefined || (Number.isInteger(num) && num >= min && num <= max);
    },
    "${ $displayName } must be between ${ $config.min } and ${$config.max }.",
    (min, max) => ({ min, max })
  );
ValidationRules
  .customRule(
    'currencyRange',
    (value, _, min, max) => isNullOrUndefined(value) || value >= min && value <= max,
    "${ $displayName } must be at least $${ $config.min.format(2, 3) } and at most $${ $config.max.format(2, 3) }.",
    (min, max) => ({ min, max })
  );

Number.prototype["format"] = function(n, x) {
  var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\.' : '$') + ')';
  return this.toFixed(Math.max(0, ~~n)).replace(new RegExp(re, 'g'), '$&,');
};
