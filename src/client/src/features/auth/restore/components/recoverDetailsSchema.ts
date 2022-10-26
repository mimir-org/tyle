import { MimirorgUserAm } from "@mimirorg/typelibrary-types";
import { TFunction } from "react-i18next";
import * as yup from "yup";
import { YupShape } from "../../../../content/forms/types/yupShape";

export const recoverDetailsSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<MimirorgUserAm>>({
    email: yup
      .string()
      .email(t("recover.details.validation.email.email"))
      .required(t("recover.details.validation.email.required")),
  });
