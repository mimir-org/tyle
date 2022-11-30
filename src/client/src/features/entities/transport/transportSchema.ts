import { YupShape } from "common/types/yupShape";
import { typeReferenceListSchema } from "features/entities/common/validation/typeReferenceListSchema";
import { FormTransportLib } from "features/entities/transport/types/formTransportLib";
import { TFunction } from "react-i18next";
import * as yup from "yup";

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
    attributes: yup.array().nullable(),
    typeReferences: typeReferenceListSchema(t("common.validation.typeReferences.name.required")),
  });
