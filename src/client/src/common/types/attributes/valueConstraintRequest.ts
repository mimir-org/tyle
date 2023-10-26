import { ConstraintType } from "./constraintType";
import { XsdDataType } from "./xsdDataType";

export interface ValueConstraintRequest {
  constraintType: ConstraintType;
  dataType: XsdDataType;
  minCount?: number;
  maxCount?: number;
  value?: string;
  valueList: string[];
  pattern?: string;
  minValue?: number;
  maxValue?: number;
}
