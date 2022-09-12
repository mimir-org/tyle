import { MimirorgUserCm } from "@mimirorg/typelibrary-types";

export const createEmptyMimirorgUserCm = (): MimirorgUserCm => ({
  id: "",
  email: "",
  firstName: "",
  lastName: "",
  phoneNumber: "",
  permissions: {},
});
