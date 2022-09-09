import { TerminalLibAm } from "@mimirorg/typelibrary-types";

export const createEmptyTerminalLibAm = (): TerminalLibAm => ({
  name: "",
  parentId: "",
  typeReferences: [],
  color: "",
  description: "",
  attributeIdList: [],
  companyId: 0,
});
