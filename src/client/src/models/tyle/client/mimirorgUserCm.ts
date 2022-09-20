import { MimirorgUserCm } from "@mimirorg/typelibrary-types";

export const createEmptyMimirorgUserCm = (): MimirorgUserCm => ({
  id: "",
  email: "",
  firstName: "",
  lastName: "",
  companyId: 0,
  companyName: "",
  purpose: "",
  permissions: {},
});
