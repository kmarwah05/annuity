import { Inputs } from "scripts/inputs";
import { inject } from "aurelia-framework";

@inject(Inputs)
export class App {
  inputs: Inputs;

  constructor(inputs){
    this.inputs = inputs;
  }

  onSubmit() {
    console.log(this.inputs.age);
  }
}
