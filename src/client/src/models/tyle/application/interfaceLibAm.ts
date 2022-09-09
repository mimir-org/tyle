import { Aspect, InterfaceLibAm } from "@mimirorg/typelibrary-types";

export const createEmptyInterfaceLibAm = (): InterfaceLibAm => ({
  name: "",
  rdsName: "",
  rdsCode: "",
  purposeName: "",
  aspect: Aspect.None,
  companyId: 0,
  terminalId: "",
  attributeIdList: [],
  description: "",
  typeReferences: [],
  parentId: "",
});
