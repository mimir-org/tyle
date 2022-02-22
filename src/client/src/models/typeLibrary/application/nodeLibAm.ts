import { Aspect } from "../enums/aspect";
import { State } from "../enums/state";
import { NodeTerminalLibAm } from "./nodeTerminalLibAm";
import { SelectedAttributePredefinedLibAm } from "./selectedAttributePredefinedLibAm";

export interface NodeLibAm {
  name: string;
  rdsId: string;
  purposeId: string;
  parentId: string;
  version: string;
  firstVersionId: string;
  aspect: Aspect;
  state: State;
  companyId: number;
  description: string;
  blobId: string;
  attributeAspectId: string;
  nodeTerminals: NodeTerminalLibAm[];
  attributeIdList: string[];
  selectedAttributePredefined: SelectedAttributePredefinedLibAm[];
  simpleIdList: string[];
  collectionIdList: string[];
  updatedBy: string;
  updated: string | null;
  created: string;
  createdBy: string;
}
