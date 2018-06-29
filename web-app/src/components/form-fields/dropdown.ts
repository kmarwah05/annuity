import { bindable } from "aurelia-framework";

export class Dropdown {
  @bindable dropdownId: string;
  @bindable labelText: string;
  @bindable placeholderText: string;

  constructor() {
    window.addEventListener("click", (event) => {
      let dropdown = document.getElementById(this.dropdownId);
      if (dropdown == null) { return; }
      let button = dropdown.previousElementSibling;
      if ((event.target as HTMLElement) != button) {
        dropdown.classList.remove("form__show");
        button.classList.remove("form__dropbtn--open");
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
    dropdown.classList.toggle("form__show");
    button.classList.toggle("form__dropbtn--open");
    if (dropdown.classList.contains("form__show")) {
      button.lastElementChild.innerHTML = "&#9650;";
    } else {
      button.lastElementChild.innerHTML = "&#9660;";
    }
  }
}
