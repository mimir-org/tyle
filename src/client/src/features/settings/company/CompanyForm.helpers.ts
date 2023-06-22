import { MimirorgCompanyAm, MimirorgCompanyCm } from "@mimirorg/typelibrary-types";
import { toast } from "complib/data-display";
import { FileInfo, toBase64 } from "complib/inputs/file/FileComponent";
import { useTranslation } from "react-i18next";

export interface FormMimirorgCompany extends Omit<MimirorgCompanyAm, "managerId" | "logo"> {
  logo: FileInfo | null;
}

export const encodeFile = async (addedFile: File): Promise<FileInfo | null> => {
  if (!(addedFile.name.endsWith(".svg") || addedFile.type === "image/svg+xml")) {
    toast.error(`Incorrect filetype: ${addedFile.type}`);
    return null;
  }

  const bytes = await toBase64(addedFile);
  return {
    fileName: addedFile.name,
    fileSize: addedFile.size,
    file: bytes != null ? bytes.toString() : null,
    contentType: addedFile.type,
  };
};

export const createEmptyFormMimirorgCompany = (): Omit<FormMimirorgCompany, "secret"> => ({
  name: "",
  displayName: "",
  description: "",
  domain: "",
  logo: null,
  homePage: "",
});

export const mapCompanyCmToFormCompany = (
  companyCm: MimirorgCompanyCm | undefined
): Omit<FormMimirorgCompany, "secret"> => {
  if (companyCm === undefined) return createEmptyFormMimirorgCompany();

  const logoFromCm = companyCm.logo
    ? {
        fileName: companyCm.id + ".svg",
        fileSize: companyCm.logo.length,
        file: "data:image/svg+xml;base64," + companyCm.logo,
        contentType: "image/svg+xml",
      }
    : null;

  return {
    name: companyCm.name,
    displayName: companyCm.displayName,
    description: companyCm.description,
    domain: companyCm.domain,
    logo: logoFromCm,
    homePage: companyCm.homePage,
  };
};

export const mapFormCompanyToCompanyAm = (
  formCompany: FormMimirorgCompany,
  userId: string,
  secret: string
): MimirorgCompanyAm => {
  let logo = "";

  if (formCompany.logo?.file) {
    const index = formCompany.logo.file.indexOf("base64,") + "base64,".length;
    if (index !== -1) logo = formCompany.logo.file.slice(index);
  }

  return {
    ...formCompany,
    logo: logo,
    managerId: userId,
    secret: secret,
  };
};

export const useCreatingToast = () => {
  const { t } = useTranslation("settings");

  return (updatingPromise: Promise<unknown>) =>
    toast.promise(updatingPromise, {
      loading: t("company.creating.loading"),
      success: t("company.creating.success"),
      error: t("company.creating.error"),
    });
};

export const useUpdateToast = () => {
  const { t } = useTranslation("settings");

  return (updatingPromise: Promise<unknown>) =>
    toast.promise(updatingPromise, {
      loading: t("company.updating.loading"),
      success: t("company.updating.success"),
      error: t("company.updating.error"),
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
