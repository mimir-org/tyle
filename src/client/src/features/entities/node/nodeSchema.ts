import { YupShape } from "common/types/yupShape";
import { TFunction } from "react-i18next";
import * as yup from "yup";
import { typeReferenceListSchema } from "../common/validation/typeReferenceListSchema";
import { FormNodeLib } from "./types/formNodeLib";

export const nodeSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<FormNodeLib>>({
    name: yup.string().max(60, t("node.validation.name.max")).required(t("node.validation.name.required")),
    rdsName: yup.string().required(t("node.validation.rdsName.required")),
    rdsCode: yup.string().required(t("node.validation.rdsCode.required")),
    purposeName: yup.string().required(t("node.validation.purposeName.required")),
    aspect: yup.number().required(t("node.validation.aspect.required")),
    companyId: yup.number().min(1, t("node.validation.companyId.min")).required(),
    description: yup.string().max(500, t("node.validation.description.max")),
    symbol: yup.string(),
    parentId: yup.string().nullable(),
    nodeTerminals: yup.array().of(
      yup.object().shape({
        terminalId: yup.string().required(t("node.validation.nodeTerminals.terminalId.required")),
      })
    ),
    attributes: yup.array().nullable(),
    typeReferences: typeReferenceListSchema(t("validation.typeReferences.name.required")),
  });
