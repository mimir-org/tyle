import { toast } from "@mimirorg/component-library";
import { useGetCurrentUser } from "api/user.queries";
import { useTranslation } from "react-i18next";
import { UserRequest } from "types/authentication/userRequest";
import { UserView } from "types/authentication/userView";

export const useUserQuery = () => {
  return useGetCurrentUser();
};

export const mapUserViewToRequest = (user: UserView): UserRequest => ({
  email: user.email,
  password: "",
  confirmPassword: "",
  firstName: user.firstName,
  lastName: user.lastName,
  purpose: user.purpose,
});

export const useUpdatingToast = () => {
  const { t } = useTranslation("settings");

  return (updatingPromise: Promise<unknown>) =>
    toast.promise(updatingPromise, {
      loading: t("usersettings.updating.loading"),
      success: t("usersettings.updating.success"),
      error: t("usersettings.updating.error"),
    });
};

export const addDummyPasswordToUserAm = (user: UserRequest): UserRequest => ({
  ...user,
  password: "DummyPassword1234",
  confirmPassword: "DummyPassword1234",
});
