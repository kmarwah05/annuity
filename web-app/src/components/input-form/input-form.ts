import { bindable , bindingMode, Container} from "aurelia-framework";
import { FundingSource, Sex, FilingStatus, Inputs } from "scripts/inputs";
import { Validator } from "scripts/validator";
import { App } from "app";
import { Module } from "webpack";

export class InputForm {
  // Bring types into VM scope
  private Sex: typeof Sex = Sex;
  private FilingStatus: typeof FilingStatus = FilingStatus;
  private FundingSource: typeof FundingSource = FundingSource;
  private AnnuityType: typeof AnnuityType = AnnuityType;

  inputs: Inputs;
  annuityType: AnnuityType = AnnuityType.Immediate;
  
  constructor() {
    this.inputs = new Inputs();
  }

  clickedSubmit() {
    if (Validator.areValidInputs(this.inputs)) {
      ((Container.instance as any).viewModel as App).onSubmit(this.inputs);
    } else {
      console.log("Invalid inputs");
    }
  }

  selected(type: AnnuityType) {
    this.annuityType = type;
    let immediate = document.getElementById("immediate");
    let deferred = document.getElementById("deferred");
    let label = document.getElementById("looking-for");
    if (type == AnnuityType.Deferred) {
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

enum AnnuityType {
  Immediate = 0,
  Deferred
}
