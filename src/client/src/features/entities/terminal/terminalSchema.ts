import { TFunction } from "i18next";
import * as yup from "yup";

export const terminalSchema = (t: TFunction<"translation">) =>
  yup.object({
    name: yup.string().required(),

    //description: yup.string(),

    classifiers: yup.array().required(),

    //purpose: yup.string(),

    //notation: yup.string(),

    //symbol: yup.string(),

    //aspect: yup.string(),

    //medium: yup.string(),

    qualifier: yup.number().min(0).required(),

    attributes: yup.array().required()
  });
