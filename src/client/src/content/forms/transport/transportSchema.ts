import { TFunction } from "react-i18next";
import * as yup from "yup";
import { typeReferenceListSchema } from "../common/validation/typeReferenceListSchema";
import { valueObjectListSchema } from "../common/validation/valueObjectListSchema";
import { YupShape } from "../types/yupShape";
import { FormTransportLib } from "./types/formTransportLib";

export const transportSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<FormTransportLib>>({
    name: yup.string().max(60, t("transport.validation.name.max")).required(t("transport.validation.name.required")),
    rdsName: yup.string().required(t("transport.validation.rdsName.required")),
    rdsCode: yup.string().required(t("transport.validation.rdsCode.required")),
    purposeName: yup.string().required(t("transport.validation.purposeName.required")),
    aspect: yup.number().required(t("transport.validation.aspect.required")),
    companyId: yup.number().min(1, t("transport.validation.companyId.min")).required(),
    terminalId: yup.string().required(t("transport.validation.terminalId.required")),
    description: yup.string().max(500, t("transport.validation.description.max")),
    parentId: yup.string().nullable(),
    attributeIdList: valueObjectListSchema(t("validation.attributeIdList.value.required")),
    typeReferences: typeReferenceListSchema(t("validation.typeReferences.name.required")),
  });
