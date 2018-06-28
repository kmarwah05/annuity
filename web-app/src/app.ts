import {PLATFORM} from 'aurelia-pal';

export class App {
  viewModel: string = PLATFORM.moduleName('components/input-form/input-form');
}
