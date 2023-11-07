import { TFunction } from "i18next";
import { DESCRIPTION_LENGTH, IRI_LENGTH, NAME_LENGTH, NOTATION_LENGTH } from "types/common/stringLengthConstants";
import * as yup from "yup";

export const terminalSchema = (t: TFunction<"translation">) =>
  yup.object({
    name: yup
      .string()
      .max(NAME_LENGTH, t("common.validation.name.max", { length: NAME_LENGTH }))
      .required(t("common.validation.name.required")),

    description: yup
      .string()
      .max(DESCRIPTION_LENGTH, t("common.validation.description.max", { length: DESCRIPTION_LENGTH })),

    notation: yup.string().max(NOTATION_LENGTH, t("common.validation.notation.max", { length: NOTATION_LENGTH })),

    symbol: yup.string().max(IRI_LENGTH, t("common.validation.symbol.max", { length: IRI_LENGTH })),

    qualifier: yup.number().required(),
  });
