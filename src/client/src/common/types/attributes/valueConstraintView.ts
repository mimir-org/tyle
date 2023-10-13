import { ConstraintType } from "./constraintType";
import { XsdDataType } from "./xsdDataType";

export interface ValueConstraintView {
  constraintType: ConstraintType;
  dataType: XsdDataType;
  minCount?: number;
  maxCount?: number;
  value?: string | number | boolean;
  valueList?: string[] | number[];
  pattern?: string;
  minValue?: number;
  maxValue?: number;
}
