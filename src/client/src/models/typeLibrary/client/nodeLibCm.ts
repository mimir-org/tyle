import { Aspect } from "../enums/aspect";
import { State } from "../enums/state";
import { AttributeAspectLibCm } from "./attributeAspectLibCm";
import { AttributeLibCm } from "./attributeLibCm";
import { NodeTerminalLibCm } from "./nodeTerminalLibCm";
import { SimpleLibCm } from "./simpleLibCm";
import { SelectedAttributePredefinedLibCm } from "./selectedAttributePredefinedLibCm";

export interface NodeLibCm {
  id: string;
  iri: string;
  name: string;
  rdsCode: string;
  rdsName: string;
  purposeName: string;
  parentIri: string;
  parentName: string;
  version: string;
  firstVersionId: string;
  aspect: Aspect;
  state: State;
  companyId: number;
  description: string;
  symbol: string;
  attributeAspectIri: string;
  attributeAspect: AttributeAspectLibCm;
  updatedBy: string;
  updated: string | null;
  created: string;
  createdBy: string;
  attributes: AttributeLibCm[];
  nodeTerminals: NodeTerminalLibCm[];
  simples: SimpleLibCm[];
  selectedAttributePredefined: SelectedAttributePredefinedLibCm[];
  kind: string;
}
