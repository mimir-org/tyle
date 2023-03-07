import { MimirorgCompanyAm } from "@mimirorg/typelibrary-types";
import { toast } from "complib/data-display";
import { FileInfo } from "complib/inputs/file/FileComponent";
import { useTranslation } from "react-i18next";

export interface FormMimirorgCompany extends Omit<MimirorgCompanyAm, "managerId" | "secret" | "logo"> {
  logo: FileInfo | null
}

export const createEmptyFormMimirorgCompany = (): FormMimirorgCompany => ({
  name: "",
  displayName: "",
  description: "",
  domain: "",
  logo: null,
  homePage: "",
});

export const mapFormCompanyToCompanyAm = (formCompany: FormMimirorgCompany, userId: string): MimirorgCompanyAm => {
  let logo = "";

  if (formCompany.logo?.file) {
    const index = formCompany.logo.file.indexOf("base64,") + "base64,".length;
    if (index != -1)
      logo = formCompany.logo.file.slice(index);
  }

  return {
    ...formCompany,
    logo: logo,
    managerId: userId,
    secret: "hush"
  }
};

export const useCreatingToast = () => {
    const { t } = useTranslation("settings");
  
    return (updatingPromise: Promise<unknown>) =>
      toast.promise(updatingPromise, {
        loading: t("createCompany.creating.loading"),
        success: t("createCompany.creating.success"),
        error: t("createCompany.creating.error"),
      });
  };