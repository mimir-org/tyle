import { YupShape } from "common/types/yupShape";
import { TFunction } from "i18next";
import * as yup from "yup";
import { FormAttributeGroupLib } from "./formAttributeGroupLib";

export const attributeGroupSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<FormAttributeGroupLib>>({
    name: yup.string().max(120, t("common.validation.name.max")).required(t("common.validation.name.required")),
    description: yup.string().max(500, t("common.validation.description.max")).nullable(),
  });
