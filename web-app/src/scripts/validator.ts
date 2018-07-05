import { Inputs } from "./inputs";

export class Validator {
  static areValidInputs(inputs: Inputs) {
    return (
      inputs.isMale != undefined &&
      inputs.currentAge != undefined &&
      inputs.filingStatus != undefined &&
      inputs.income != undefined &&
      inputs.amount != undefined &&
      inputs.withdrawalUntil != undefined &&
      (
        (
          inputs.taxType != undefined
        ) ||
        (
          inputs.retireAge != undefined
        )
      )
    );
  }
}
