import { HasCardinality } from "types/common/hasCardinality";
import { ConstraintType } from "./constraintType";
import { XsdDataType } from "./xsdDataType";

export interface ValueConstraintView extends HasCardinality {
  constraintType: ConstraintType;
  dataType: XsdDataType;
  value: string | number | boolean | null;
  valueList: string[] | number[] | null;
  pattern: string | null;
  minValue: number | null;
  maxValue: number | null;
}
