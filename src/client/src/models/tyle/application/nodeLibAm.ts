import { Aspect, NodeLibAm } from "@mimirorg/typelibrary-types";

export const createEmptyNodeLibAm = (): NodeLibAm => ({
  name: "",
  rdsName: "",
  rdsCode: "",
  purposeName: "",
  aspect: Aspect.None,
  companyId: 0,
  simpleIdList: [],
  attributeIdList: [],
  nodeTerminals: [],
  selectedAttributePredefined: [],
  description: "",
  symbol: "",
  attributeAspectIri: "",
  typeReferences: [],
  parentId: "",
});
