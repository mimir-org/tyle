import { Aspect, NodeLibAm } from "@mimirorg/typelibrary-types";

export const createEmptyNodeLibAm = (): NodeLibAm => ({
  name: "",
  rdsName: "",
  rdsCode: "",
  purposeName: "",
  aspect: Aspect.None,
  companyId: 0,
  attributeIdList: [],
  nodeTerminals: [],
  selectedAttributePredefined: [],
  description: "",
  symbol: "",
  typeReferences: [],
  parentId: "",
  version: "1.0",
});
