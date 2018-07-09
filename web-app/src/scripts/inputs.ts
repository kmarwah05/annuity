/*
 * Stores input data
 */

export class Inputs {

  currentAge: number = undefined; // Age really is just a number in TypeScript
  income: number = undefined;
  isMale: boolean = undefined;
  filingStatus: FilingStatus = undefined;

  isDeferred: boolean = false;
  amount: number = undefined;

  // Immediate
  taxType: number = undefined;

  // Deferred
  retireAge: number = undefined;

  withdrawalUntil: number = undefined;

  riders: string[] = [];

  complete(isFixed: boolean, endAge: number) {
    this.withdrawalUntil = isFixed ?
    (this.isDeferred ?
      endAge - this.retireAge :
      endAge - this.currentAge)
    : 0;

    if (!this.isDeferred) {
      this.retireAge = Number(this.currentAge) + 1;
    }
  }
}

export enum TaxStatus {
  roth = 0,
  unqualified=1,
  qualified=2
}

export enum FilingStatus {
  Joint = 0,
  HeadOfHousehold,
  Unmarried,
  MarriedSeparate
}
