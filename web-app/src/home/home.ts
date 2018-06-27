import { FundingSource, Sex, FilingStatus, Inputs } from "scripts/inputs";

export class Home {
  // Bring types into VM scope
  private Sex: typeof Sex = Sex;
  private FilingStatus: typeof FilingStatus = FilingStatus;
  private FundingSource: typeof FundingSource = FundingSource;
  private AnnuityType: typeof AnnuityType = AnnuityType;

  private _annuityType: AnnuityType = AnnuityType.Immediate;

  inputs: Inputs;
  
  get annuityType() {
    return this._annuityType;
  }

  set annuityType(type: AnnuityType) {
    this.showInputsFor(this._annuityType);
    this._annuityType = type;
  }

  constructor() {
    this.inputs = new Inputs();
  }

  showInputsFor(annuityType: AnnuityType) {
    let ids = ["deferred-annuity-info", "immediate-annuity-info"];
    let [hidden, shown] = annuityType == AnnuityType.Immediate ?
      ids :
      ids.reverse();
    document.getElementById(hidden).classList.add("form__section--hidden");
    document.getElementById(shown).classList.remove("form__section--hidden");
  }

  print(): void {
    Object.keys(this.inputs).forEach(element => {
      console.log(element);
      console.log(this.inputs[element]);
    });
  }
}

enum AnnuityType {
  Immediate = 1,
  Deferred
}
