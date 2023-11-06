import { TFunction } from "i18next";
import { UserRequest } from "types/authentication/userRequest";
import { YupShape } from "types/yupShape";
import * as yup from "yup";

export const userSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<UserRequest>>({
    firstName: yup.string().required(t("usersettings.validation.firstName.required")),
    lastName: yup.string().required(t("usersettings.validation.lastName.required")),
  });
