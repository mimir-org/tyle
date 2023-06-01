import { YupShape } from "common/types/yupShape";
import { FormTerminalLib } from "features/entities/terminal/types/formTerminalLib";
import { TFunction } from "react-i18next";
import * as yup from "yup";

export const terminalSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<FormTerminalLib>>({
    name: yup.string().max(120, t("common.validation.name.max")).required(t("common.validation.name.required")),
    typeReference: yup.string().max(255),
    color: yup.string().max(7).required(t("terminal.validation.color.required")),
    description: yup.string().max(500, t("common.validation.description.max")),
    attributes: yup.array().nullable(),
  });
