import {PLATFORM} from 'aurelia-pal';

export class App {
  viewModel: string = PLATFORM.moduleName('home/home');
}
