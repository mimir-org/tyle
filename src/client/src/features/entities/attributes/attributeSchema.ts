import { YupShape } from "common/types/yupShape";
import { TFunction } from "i18next";
import * as yup from "yup";
import { AttributeFormFields } from "./AttributeForm.helpers";
import { DESCRIPTION_LENGTH, NAME_LENGTH } from "common/types/common/stringLengthConstants";

export const attributeSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<AttributeFormFields>>({
    name: yup.string().max(NAME_LENGTH, t("common.validation.name.max")).required(t("common.validation.name.required")),
    description: yup.string().max(DESCRIPTION_LENGTH, t("common.validation.description.max")),
    constraintType: yup.number().when("valueConstraint", {
      is: true,
      then: (schema) => schema.required(),
    }),
  });
