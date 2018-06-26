import {RouterConfiguration, Router} from 'aurelia-router';
import {PLATFORM} from 'aurelia-pal';

export class App {
  router: Router;

  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = 'Guaranteed Income';
    config.options.pushState = true;

    config.map([
      { route: ['', 'home'], name: 'home', moduleId: PLATFORM.moduleName('home/home'), title: 'Home' },
      { route: 'results', name: 'results', moduleId: PLATFORM.moduleName('results/results'), title: 'Results' }
    ]);

    this.router = router;
  }

}
