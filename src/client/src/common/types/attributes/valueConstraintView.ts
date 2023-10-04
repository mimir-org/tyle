import { ConstraintType } from "./constraintType";
import { XsdDataType } from "./xsdDataType";

export interface ValueConstraintView {
    constraintType: ConstraintType;
    dataType: XsdDataType;
    minCount: number | null;
    maxCount: number | null;
    value: string | number | boolean | null;
    valueList: string[] | number[] | null;
    pattern: string | null;
    minValue: number | null;
    maxValue: number | null;
    minInclusive: boolean | null;
    maxInclusive: boolean | null;
}