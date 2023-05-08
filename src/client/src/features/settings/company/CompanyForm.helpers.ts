import { MimirorgCompanyAm, MimirorgCompanyCm } from "@mimirorg/typelibrary-types";
import { toast } from "complib/data-display";
import { FileInfo } from "complib/inputs/file/FileComponent";
import { useCreateCompany, useUpdateCompany } from "external/sources/company/company.queries";
import { useTranslation } from "react-i18next";

export interface FormMimirorgCompany extends Omit<MimirorgCompanyAm, "managerId" | "logo"> {
  logo: FileInfo | null;
}

export const createEmptyFormMimirorgCompany = (): Omit<FormMimirorgCompany, "secret"> => ({
  name: "",
  displayName: "",
  description: "",
  domain: "",
  logo: null,
  homePage: "",
});

export const mapCompanyCmToFormCompany = (companyCm: MimirorgCompanyCm | undefined): Omit<FormMimirorgCompany, "secret"> => {
  if (companyCm == undefined) return createEmptyFormMimirorgCompany();
  return {
    name: companyCm.name,
    displayName: companyCm.displayName,
    description: companyCm.description,
    domain: companyCm.domain,
    logo: {
      fileName: "logo.svg",
      fileSize: companyCm.logo.length,
      file: companyCm.logo,
      contentType: "image/svg+xml"
    },
    homePage: companyCm.homePage
  }
}

export const mapFormCompanyToCompanyAm = (formCompany: FormMimirorgCompany, userId: string, secret: string): MimirorgCompanyAm => {
  let logo = "";

  if (formCompany.logo?.file) {
    const index = formCompany.logo.file.indexOf("base64,") + "base64,".length;
    if (index != -1) logo = formCompany.logo.file.slice(index);
  }

  return {
    ...formCompany,
    logo: logo,
    managerId: userId,
    secret: secret,
  };
};

export const useCompanyMutation = (companyId: string) => {
  const createCompanyMutation = useCreateCompany();
  const updateCompanyMutation = useUpdateCompany(String(companyId));
  return companyId == "0" ? createCompanyMutation : updateCompanyMutation;
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

export const createSecret = (length: number): string => {
  const availableCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!()?[];:#@%+=";
  let secret = "";

  const numArray = new Uint32Array(length);
  crypto.getRandomValues(numArray);
  numArray.forEach((x) => {
    secret += availableCharacters[x % availableCharacters.length];
  });

  return secret;
};

export const copySecret = (secret: string, toastText: string): void => {
  navigator.clipboard.writeText(secret);
  toast.success(toastText);
};
