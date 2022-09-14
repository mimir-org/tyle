import { Select } from "@mimirorg/typelibrary-types";
import { TFunction } from "react-i18next";
import * as yup from "yup";
import { typeReferenceListSchema } from "../common/validation/typeReferenceListSchema";
import { YupShape } from "../types/yupShape";
import { FormAttributeLib } from "./types/formAttributeLib";

export const attributeSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<FormAttributeLib>>({
    name: yup.string().max(30, t("attribute.validation.name.max")).required(t("attribute.validation.name.required")),
    aspect: yup.number().required(t("attribute.validation.aspect.required")),
    discipline: yup.number().required(t("attribute.validation.discipline.required")),
    select: yup.string().required(t("attribute.validation.select.required")),
    attributeQualifier: yup.string().required(t("attribute.validation.attributeQualifier.required")),
    attributeSource: yup.string().required(t("attribute.validation.attributeSource.required")),
    attributeCondition: yup.string().required(t("attribute.validation.attributeCondition.required")),
    attributeFormat: yup.string().required(t("attribute.validation.attributeFormat.required")),
    companyId: yup.number().min(1, t("attribute.validation.companyId.min")).required(),
    typeReferences: typeReferenceListSchema(t("validation.typeReferences.name.required")),
    selectValues: yup.array().when("select", {
      is: (selectValue: Select) => selectValue != Select.None,
      then: yup
        .array()
        .min(1, t("attribute.validation.selectValues.min"))
        .of(
          yup.object().shape({
            value: yup.string().required(t("attribute.validation.selectValues.value.required")),
          })
        ),
    }),
    unitIdList: yup.array().of(
      yup.object().shape({
        value: yup.string().required(t("attribute.validation.unitIdList.value.required")),
      })
    ),
  });
