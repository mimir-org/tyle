import { Aspect } from "../enums/aspect";
import { NodeTerminalLibAm } from "./nodeTerminalLibAm";
import { SelectedAttributePredefinedLibAm } from "./selectedAttributePredefinedLibAm";

export interface NodeLibAm {
  name: string;
  rdsName: string;
  rdsCode: string;
  purposeName: string;
  aspect: Aspect;
  companyId: number;
  simpleIdList: string[];
  attributeIdList: string[];
  nodeTerminals: NodeTerminalLibAm[];
  selectedAttributePredefined: SelectedAttributePredefinedLibAm[];
  description: string;
  symbol: string;
  attributeAspectIri: string;
  contentReferences: string[];
  parentId: string;
}

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
  contentReferences: [],
  parentId: "",
});
