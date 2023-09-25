import { YupShape } from "common/types/yupShape";
import { TFunction } from "i18next";
import * as yup from "yup";
import { FormAttributeGroupLib } from "./types/formAttributeGroupLib";

const formUnitHelperShape = {
  name: yup.string().max(120).required(),
  description: yup.string().max(500).nullable(),
  symbol: yup.string().max(30).nullable(),
  unitId: yup.string().required(),
};

export const attributeGroupSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<FormAttributeGroupLib>>({
    name: yup.string().max(120, t("common.validation.name.max")).required(t("common.validation.name.required")),
    // typeReference: yup.string().max(255).nullable(),
    // description: yup.string().max(500, t("common.validation.description.max")).nullable(),
    // units: yup.array().of(yup.object(formUnitHelperShape)).nullable(),
    // defaultUnit: yup.object(formUnitHelperShape).nullable(),
  });
