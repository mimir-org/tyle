import { TFunction } from "react-i18next";
import * as yup from "yup";
import { typeReferenceListSchema } from "../common/validation/typeReferenceListSchema";
import { valueObjectListSchema } from "../common/validation/valueObjectListSchema";
import { YupShape } from "../types/yupShape";
import { FormInterfaceLib } from "./types/formInterfaceLib";

export const interfaceSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<FormInterfaceLib>>({
    name: yup.string().max(60, t("interface.validation.name.max")).required(t("interface.validation.name.required")),
    rdsName: yup.string().required(t("interface.validation.rdsName.required")),
    rdsCode: yup.string().required(t("interface.validation.rdsCode.required")),
    purposeName: yup.string().required(t("interface.validation.purposeName.required")),
    aspect: yup.number().required(t("interface.validation.aspect.required")),
    companyId: yup.number().min(1, t("interface.validation.companyId.min")).required(),
    terminalId: yup.string().required(t("interface.validation.terminalId.required")),
    description: yup.string().max(500, t("interface.validation.description.max")),
    parentId: yup.string().nullable(),
    attributeIdList: valueObjectListSchema(t("validation.attributeIdList.value.required")),
    typeReferences: typeReferenceListSchema(t("validation.typeReferences.name.required")),
  });
