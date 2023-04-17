import { TFunction } from "react-i18next";
import * as yup from "yup";
import { YupShape } from "../../../common/types/yupShape";
import { FormAttributeLib } from "./types/formAttributeLib";

export const attributeSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<FormAttributeLib>>({
    name: yup.string().max(60, t("attribute.validation.name.max")).required(t("attribute.validation.name.required")),
  });
