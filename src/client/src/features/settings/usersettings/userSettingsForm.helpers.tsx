import { MimirorgUserAm, MimirorgUserCm } from "@mimirorg/typelibrary-types";
import { toast } from "complib/data-display";
import { useGetCurrentUser } from "external/sources/user/user.queries"
import { useTranslation } from "react-i18next";

export const useUserQuery = () => {
    return useGetCurrentUser();
};

export const mapMimirorgUserCmToAm = (user: MimirorgUserCm): MimirorgUserAm => ({
    email: user.email,
    password: "",
    confirmPassword: "",
    firstName: user.firstName,
    lastName: user.lastName,
    companyId: user.companyId,
    purpose: user.purpose
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

export const addDummyPasswordToUserAm = (user: MimirorgUserAm): MimirorgUserAm => ({
    ...user,
    password: "DummyPassword1234",
    confirmPassword: "DummyPassword1234"
});