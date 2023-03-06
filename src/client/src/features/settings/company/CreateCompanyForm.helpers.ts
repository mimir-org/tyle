import { MimirorgCompanyAm } from "@mimirorg/typelibrary-types";
import { toast } from "complib/data-display";
import { useTranslation } from "react-i18next";

export type FormMimirorgCompany = Omit<MimirorgCompanyAm, "managerId" | "secret">;

export const createEmptyFormMimirorgCompany = (): FormMimirorgCompany => ({
  name: "",
  displayName: "",
  description: "",
  domain: "",
  logo: "",
  homePage: "",
});

export const mapFormCompanyToCompanyAm = (formCompany: FormMimirorgCompany, userId: string): MimirorgCompanyAm => ({
    ...formCompany,
    managerId: userId,
    secret: "hush"
});

export const useCreatingToast = () => {
    const { t } = useTranslation("settings");
  
    return (updatingPromise: Promise<unknown>) =>
      toast.promise(updatingPromise, {
        loading: t("createCompany.creating.loading"),
        success: t("createCompany.creating.success"),
        error: t("createCompany.creating.error"),
      });
  };