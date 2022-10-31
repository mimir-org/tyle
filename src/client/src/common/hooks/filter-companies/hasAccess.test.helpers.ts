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
  homePage: "",
  iris: [],
});

export const createEmptyMimirorgUserCm = (): MimirorgUserCm => ({
  id: "",
  email: "",
  firstName: "",
  lastName: "",
  companyId: 0,
  companyName: "",
  purpose: "",
  permissions: {},
  roles: [],
});
