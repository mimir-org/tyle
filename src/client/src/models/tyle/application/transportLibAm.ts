import { Aspect, TransportLibAm } from "@mimirorg/typelibrary-types";

export const createEmptyTransportLibAm = (): TransportLibAm => ({
  name: "",
  rdsName: "",
  rdsCode: "",
  purposeName: "",
  aspect: Aspect.None,
  companyId: 0,
  terminalId: "",
  attributes: [],
  description: "",
  typeReferences: [],
  parentId: "",
  version: "1.0",
});
