import { Inputs } from "scripts/inputs";
import { Validator } from "scripts/validator";

export class App {
  private Pages: typeof Pages = Pages;

  inputs: Inputs = new Inputs();
  currentPage: Pages = Pages.Input;

  onSubmit() {
    if (Validator.areValidInputs(this.inputs)) {
      this.currentPage = Pages.Results;
    }
  }

  onNewInputs() {
    this.currentPage = Pages.Input;
  }
}

enum Pages {
  Input = 0,
  Results
}
