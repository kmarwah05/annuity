import { Inputs } from "scripts/inputs";

export class App {
  private Pages: typeof Pages = Pages;

  inputs: Inputs;
  currentPage: Pages = Pages.Input;

  onSubmit(inputs: Inputs) {
    this.inputs = inputs;
    this.currentPage = Pages.Results;
  }
}

enum Pages {
  Input = 0,
  Results
}
