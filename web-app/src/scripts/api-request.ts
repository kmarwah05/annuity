import { json} from 'aurelia-fetch-client';
import { Inputs } from "./inputs";
import { Data } from "./data";
import { HttpService } from './http-service';

export class APIRequest {
  static response: Data;

  static async postInputs(inputs: Inputs) {
    let client = new HttpService();
    client.configureClient();

    this.response = await client.fetch(json(inputs));
  }
}
