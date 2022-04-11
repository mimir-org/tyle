import { Aspect } from "../enums/aspect";
import { State } from "../enums/state";
import { NodeTerminalLibAm } from "./nodeTerminalLibAm";
import { SelectedAttributePredefinedLibAm } from "./selectedAttributePredefinedLibAm";

export interface NodeLibAm {
  name: string;
  rdsId: string;
  purposeId: string;
  parentId: string | null;
  version: string | null;
  firstVersionId: string | null;
  aspect: Aspect;
  state: State;
  companyId: number;
  description: string;
  blobId: string;
  attributeAspectId: string | null;
  nodeTerminals: NodeTerminalLibAm[];
  attributeIdList: string[];
  selectedAttributePredefined: SelectedAttributePredefinedLibAm[] | null;
  simpleIdList: string[] | null;
  collectionIdList: string[] | null;
  updatedBy: string | null;
  updated: string | null;
  created: string;
  createdBy: string;
}
