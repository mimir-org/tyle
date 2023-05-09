import { MimirorgCompanyAm, MimirorgCompanyCm } from "@mimirorg/typelibrary-types";
import axios from "axios";
import { toast } from "complib/data-display";
import { FileInfo, toBase64 } from "complib/inputs/file/FileComponent";
import { useCreateCompany, useUpdateCompany } from "external/sources/company/company.queries";
import { useTranslation } from "react-i18next";

export interface FormMimirorgCompany extends Omit<MimirorgCompanyAm, "managerId" | "logo"> {
  logo: FileInfo | null;
}

export const encodeFile = async (addedFile: File): Promise<FileInfo | null> => {
  if (!(addedFile.name.endsWith(".svg") || addedFile.type == "image/svg+xml")) {
    toast.error(`Incorrect filetype: ${addedFile.type}`);
    return null;
  }

  const bytes = await toBase64(addedFile);
  const fileToBeAdded: FileInfo = {
    fileName: addedFile.name,
    fileSize: addedFile.size,
    file: bytes != null ? bytes.toString() : null,
    contentType: addedFile.type,
  };

  return fileToBeAdded;
};

export const createEmptyFormMimirorgCompany = (): Omit<FormMimirorgCompany, "secret"> => ({
  name: "",
  displayName: "",
  description: "",
  domain: "",
  logo: null,
  homePage: "",
});

export const mapCompanyCmToFormCompany = async (companyCm: MimirorgCompanyCm | undefined): Promise<Omit<FormMimirorgCompany, "secret">> => {
  if (companyCm == undefined) return createEmptyFormMimirorgCompany();
  const downloadedLogo = axios.get(companyCm.logo, { responseType: "blob" , headers: { "Content-Type": "image/svg+xml" }}).then((res) => {
    return encodeFile(new File([res.data], "logo.svg", { type: "image/svg+xml" }));
  }).catch((error) => {
    console.log(error);
    return null;
  });
  return {
    name: companyCm.name,
    displayName: companyCm.displayName,
    description: companyCm.description,
    domain: companyCm.domain,
    logo: await downloadedLogo,
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

export const useCreatingToast = (companyId: string) => {
  const { t } = useTranslation("settings");

  if (companyId == "0") {
    return (updatingPromise: Promise<unknown>) =>
    toast.promise(updatingPromise, {
      loading: t("company.creating.loading"),
      success: t("company.creating.success"),
      error: t("company.creating.error"),
    });
  } else {
    return (updatingPromise: Promise<unknown>) =>
    toast.promise(updatingPromise, {
      loading: t("company.updating.loading"),
      success: t("company.updating.success"),
      error: t("company.updating.error"),
    });
  }
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
