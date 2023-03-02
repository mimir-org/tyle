import { MimirorgUserAm } from "@mimirorg/typelibrary-types";
import { YupShape } from "common/types/yupShape";
import { TFunction } from "react-i18next";
import * as yup from "yup";

export const userSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<MimirorgUserAm>>({
    firstName: yup.string().required(t("usersettings.validation.firstName.required")),
    lastName: yup.string().required(t("usersettings.validation.lastName.required"))
  });
