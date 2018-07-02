import { Inputs } from "scripts/inputs";

export class App {
  onSubmit(inputs: Inputs) {
    console.log(inputs.age);
  }
}
