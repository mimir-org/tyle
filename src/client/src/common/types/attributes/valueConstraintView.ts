import { ConstraintType } from "./constraintType";
import { XsdDataType } from "./xsdDataType";

export interface ValueConstraintView {
  constraintType: ConstraintType;
  dataType: XsdDataType;
  minCount: number | undefined;
  maxCount: number | undefined;
  value: string | number | boolean | undefined;
  valueList: string[] | number[] | undefined;
  pattern: string | undefined;
  minValue: number | undefined;
  maxValue: number | undefined;
}
