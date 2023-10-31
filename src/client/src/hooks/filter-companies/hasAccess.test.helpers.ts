import { MimirorgCompanyCm, MimirorgUserCm } from "@mimirorg/typelibrary-types";

export const createEmptyMimirorgCompanyCm = (): MimirorgCompanyCm => ({
  id: 0,
  name: "",
  displayName: "",
  description: "",
  manager: createEmptyMimirorgUserCm(),
  secret: "",
  domain: "",
  logo: "",
  logoUrl: "",
  homePage: "",
});

export const createEmptyMimirorgUserCm = (): MimirorgUserCm => ({
  id: "",
  email: "",
  firstName: "",
  lastName: "",
  companyId: 0,
  manageCompanies: [],
  companyName: "",
  purpose: "",
  permissions: {},
  roles: [],
});
