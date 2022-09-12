import { MimirorgCompanyCm } from "@mimirorg/typelibrary-types";
import { createEmptyMimirorgUserCm } from "./mimirorgUserCm";

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
