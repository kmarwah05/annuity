import { bindable } from "aurelia-framework";

export class Dropdown {
  @bindable dropdownId: string;
  @bindable labelText: string;
  @bindable placeholderText: string;

  constructor() {
    window.addEventListener("click", (event) => {
      let dropdown = document.getElementById(this.dropdownId);
      let button = dropdown.previousElementSibling;
      if ((event.target as HTMLElement) != button) {
        dropdown.classList.remove("form__show");
        button.lastElementChild.innerHTML = "&#9660;";
      }
      if ((event.target as HTMLElement).parentElement == dropdown) {
        button.innerHTML = (event.target as HTMLElement).innerText +
          "<span class=\"form__arrow\">&#9660;</span>";
      }
    });
  }

  dropdown() {
    let dropdown = document.getElementById(this.dropdownId);
    let button = dropdown.previousElementSibling;
    dropdown.classList.add("form__show");
    button.lastElementChild.innerHTML = "&#9650;";
  }
}
