import { Inputs } from "scripts/inputs";

export class App {
  inputs: Inputs;

  constructor() {
    this.inputs = new Inputs();
  }

  onSubmit() {
    console.log(this.inputs.age);
  }
}
