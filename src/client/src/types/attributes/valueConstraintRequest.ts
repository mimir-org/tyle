import { HasCardinality } from "types/common/hasCardinality";
import { ConstraintType } from "./constraintType";
import { XsdDataType } from "./xsdDataType";

export interface ValueConstraintRequest extends HasCardinality {
  constraintType: ConstraintType;
  dataType: XsdDataType;
  value: string | null;
  valueList: string[];
  pattern: string | null;
  minValue: number | null;
  maxValue: number | null;
}
