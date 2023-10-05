import { ConstraintType } from "./constraintType";
import { XsdDataType } from "./xsdDataType";

export interface ValueConstraintRequest {
  constraintType: ConstraintType;
  dataType: XsdDataType;
  minCount: number | null;
  maxCount: number | null;
  value: string | null;
  valueList: string[];
  pattern: string | null;
  minValue: number | null;
  maxValue: number | null;
  minInclusive: boolean | null;
  maxInclusive: boolean | null;
}
