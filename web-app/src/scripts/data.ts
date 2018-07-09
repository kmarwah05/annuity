export class Data {
  fixed: number;

  variableWorstCase: number;
  variableLowerQuartile: number;
  variableMedian: number;
  variableUpperQuartile: number;

  fixedIndexedWorstCase: number;
  fixedIndexedLowerQuartile: number;
  fixedIndexedMedian: number;
  fixedIndexedUpperQuartile: number;

  brokerageWorstCase: number;
  brokerageLowerQuartile: number;
  brokerageMedian: number;
  brokerageUpperQuartile: number;

  fixedIndexAboveBrokerage: number;
  variableAboveBrokerage: number;
  brokerageBelowFixed: number;
}
