import { ConstraintType } from "./constraintType";
import { XsdDataType } from "./xsdDataType";

export interface ValueConstraintRequest {
  constraintType: ConstraintType;
  dataType: XsdDataType;
  minCount: number | undefined;
  maxCount: number | undefined;
  value: string | undefined;
  valueList: string[];
  pattern: string | undefined;
  minValue: number | undefined;
  maxValue: number | undefined;
  minInclusive: boolean | undefined;
  maxInclusive: boolean | undefined;
}
