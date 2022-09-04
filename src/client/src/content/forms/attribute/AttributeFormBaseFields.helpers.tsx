import { Select } from "@mimirorg/typelibrary-types";
import { UseFormResetField } from "react-hook-form";
import { FormAttributeLib } from "./types/formAttributeLib";

export const onChangeSelectType = (resetField: UseFormResetField<FormAttributeLib>, value?: Select) => {
  if (value === Select.None) resetField("selectValues");
};
