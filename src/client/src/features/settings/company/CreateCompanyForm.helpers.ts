import { MimirorgCompanyAm } from "@mimirorg/typelibrary-types";

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