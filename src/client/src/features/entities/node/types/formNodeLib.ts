import { Aspect, AttributeLibAm, NodeLibAm, NodeLibCm } from "@mimirorg/typelibrary-types";
import { UpdateEntity } from "common/types/updateEntity";
import {
  FormAttributePredefinedLib,
  mapAttributePredefinedLibCmToClientModel,
  mapFormAttributePredefinedLibToApiModel,
} from "features/entities/node/types/formAttributePredefinedLib";
import {
  FormNodeTerminalLib,
  mapNodeTerminalLibCmToClientModel,
} from "features/entities/node/types/formNodeTerminalLib";
import { NodeFormMode } from "features/entities/node/types/nodeFormMode";
import { ValueObject } from "features/entities/types/valueObject";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormNodeLib extends Omit<NodeLibAm, "attributes" | "selectedAttributePredefined" | "nodeTerminals"> {
  attributes: ValueObject<UpdateEntity<AttributeLibAm>>[];
  selectedAttributePredefined: FormAttributePredefinedLib[];
  nodeTerminals: FormNodeTerminalLib[];
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formNode client-only model
 */
export const mapFormNodeLibToApiModel = (formNode: FormNodeLib): NodeLibAm => ({
  ...formNode,
  attributes: formNode.attributes.map((x) => x.value),
  selectedAttributePredefined: formNode.selectedAttributePredefined.map((x) =>
    mapFormAttributePredefinedLibToApiModel(x)
  ),
});

export const mapNodeLibCmToClientModel = (node: NodeLibCm, mode?: NodeFormMode): FormNodeLib => ({
  ...node,
  parentId: mode === "clone" ? node.id : node.parentId,
  attributes: node.attributes.map((x) => ({ value: x })),
  nodeTerminals: node.nodeTerminals.map(mapNodeTerminalLibCmToClientModel),
  selectedAttributePredefined: node.selectedAttributePredefined.map(mapAttributePredefinedLibCmToClientModel),
});

export const createEmptyFormNodeLib = (): FormNodeLib => ({
  ...emptyNodeLibAm,
  attributes: [],
  selectedAttributePredefined: [],
  nodeTerminals: [],
});

const emptyNodeLibAm: NodeLibAm = {
  name: "",
  rdsName: "",
  rdsCode: "",
  purposeName: "",
  aspect: Aspect.None,
  companyId: 0,
  attributes: [],
  nodeTerminals: [],
  selectedAttributePredefined: [],
  description: "",
  symbol: "",
  typeReferences: [],
  parentId: 0,
  version: "1.0",
};
