import { YupShape } from "common/types/yupShape";
import { TFunction } from "react-i18next";
import * as yup from "yup";
import { FormAttributeLib } from "./types/formAttributeLib";

export const attributeSchema = (t: TFunction<"translation">) => 
  yup.object<YupShape<FormAttributeLib>>({
    name: yup.string().max(120, t("common.validation.name.max")).required(t("common.validation.name.required")),
    typeReference: yup.string().max(255),
    description: yup.string().max(500, t("common.validation.description.max")),
    units: yup.array()
      .of(
        yup.object({
          name: yup.string().max(120).required(),
          description: yup.string().max(500).required(),
          symbol: yup.string().max(30).required(),
          unitId: yup.string().required(),
        })
      ),
    defaultUnit: yup.object({
      name: yup.string().max(120).required(),
      description: yup.string().max(500).required(),
      symbol: yup.string().max(30).required(),
      unitId: yup.string().required(),
    })
});
