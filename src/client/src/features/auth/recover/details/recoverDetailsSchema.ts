import { MimirorgUserAm } from "@mimirorg/typelibrary-types";
import { YupShape } from "common/types/yupShape";
import { TFunction } from "i18next";
import * as yup from "yup";

export const recoverDetailsSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<MimirorgUserAm>>({
    email: yup
      .string()
      .email(t("recover.details.validation.email.email"))
      .required(t("recover.details.validation.email.required")),
  });
