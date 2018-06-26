import { FundingSource, Sex, FilingStatus, Inputs } from "scripts/inputs";

export class Home {
  // Bring types into VM scope
  private Sex: typeof Sex = Sex;
  private FilingStatus: typeof FilingStatus = FilingStatus;
  private FundingSource: typeof FundingSource = FundingSource;

  inputs: Inputs;

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
