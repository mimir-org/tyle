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
  version: string;
  contentReferences: string[];
  parentId: string;
}
