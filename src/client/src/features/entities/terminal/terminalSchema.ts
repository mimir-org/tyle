import { YupShape } from "common/types/yupShape";
import { TFunction } from "react-i18next";
import * as yup from "yup";
import { typeReferenceListSchema } from "../common/validation/typeReferenceListSchema";
import { FormTerminalLib } from "./types/formTerminalLib";

export const terminalSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<FormTerminalLib>>({
    name: yup.string().max(60, t("terminal.validation.name.max")).required(t("terminal.validation.name.required")),
    color: yup.string().required(t("terminal.validation.color.required")),
    companyId: yup.number().min(1, t("terminal.validation.companyId.min")).required(),
    description: yup.string().max(500, t("terminal.validation.description.max")),
    parentId: yup.string().nullable(),
    attributes: yup.array().nullable(),
    typeReferences: typeReferenceListSchema(t("validation.typeReferences.name.required")),
  });
