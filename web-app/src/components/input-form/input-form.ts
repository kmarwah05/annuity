import { FundingSource, Sex, FilingStatus, Inputs } from "scripts/inputs";

export class InputForm {
  // Bring types into VM scope
  private Sex: typeof Sex = Sex;
  private FilingStatus: typeof FilingStatus = FilingStatus;
  private FundingSource: typeof FundingSource = FundingSource;
  private AnnuityType: typeof AnnuityType = AnnuityType;

  inputs: Inputs;
  annuityType: AnnuityType = AnnuityType.Immediate;

  testDropdown: Sex;

  constructor() {
    this.inputs = new Inputs();
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
