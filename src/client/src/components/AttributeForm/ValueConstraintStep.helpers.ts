import { ConstraintType } from "types/attributes/constraintType";
import { XsdDataType } from "types/attributes/xsdDataType";

export interface ValueConstraintFields {
  set: boolean;
  constraintType: ConstraintType;
  dataType: XsdDataType;
  value: string;
  valueList: ValueListItem[];
  pattern: string;
  minValue: string;
  maxValue: string;
  requireValue: boolean;
}

export interface ValueListItem {
  id: string;
  value: string;
}

export const getNotSetValueConstraintFields = (): ValueConstraintFields => ({
  set: false,
  constraintType: ConstraintType.HasSpecificValue,
  dataType: XsdDataType.String,
  value: "",
  valueList: [],
  pattern: "",
  minValue: "",
  maxValue: "",
  requireValue: false,
});

export const getEmptyValueFields = (): Omit<
  ValueConstraintFields,
  "set" | "constraintType" | "dataType" | "requireValue"
> => ({
  value: "",
  valueList: [],
  pattern: "",
  minValue: "",
  maxValue: "",
});
