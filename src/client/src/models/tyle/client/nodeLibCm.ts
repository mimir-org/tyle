import { Aspect } from "../enums/aspect";
import { State } from "../enums/state";
import { AttributeLibCm } from "./attributeLibCm";
import { NodeTerminalLibCm } from "./nodeTerminalLibCm";
import { SimpleLibCm } from "./simpleLibCm";
import { SelectedAttributePredefinedLibCm } from "./selectedAttributePredefinedLibCm";

export interface NodeLibCm {
  id: string;
  parentName: string;
  parentIri: string;
  name: string;
  version: string;
  firstVersionId: string;
  iri: string;
  attributeAspectIri: string;
  contentReferences: string[];
  rdsCode: string;
  rdsName: string;
  purposeName: string;
  aspect: Aspect;
  state: State;
  companyId: number;
  symbol: string;
  description: string;
  created: string;
  createdBy: string;
  nodeTerminals: NodeTerminalLibCm[];
  attributes: AttributeLibCm[];
  simples: SimpleLibCm[];
  selectedAttributePredefined: SelectedAttributePredefinedLibCm[];
  kind: string;
}
