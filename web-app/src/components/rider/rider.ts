import { bindable } from "aurelia-framework";

export class Rider {
  @bindable rider: string;

  toggleRider(rider: string) {
    document.getElementById(rider).classList.toggle("riders__checkbox--checked");
  }
}
