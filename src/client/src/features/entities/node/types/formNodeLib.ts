import { Aspect, AttributeLibAm, NodeLibAm, NodeLibCm } from "@mimirorg/typelibrary-types";
import { UpdateEntity } from "common/types/updateEntity";
import {
  FormSelectedAttributePredefinedLib,
  mapFormSelectedAttributePredefinedLibToApiModel,
} from "features/entities/node/types/formSelectedAttributePredefinedLib";
import { NodeFormMode } from "features/entities/node/types/nodeFormMode";
import { ValueObject } from "features/entities/types/valueObject";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormNodeLib extends Omit<NodeLibAm, "attributes" | "selectedAttributePredefined"> {
  attributes: ValueObject<UpdateEntity<AttributeLibAm>>[];
  selectedAttributePredefined: FormSelectedAttributePredefinedLib[];
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formNode client-only model
 */
export const mapFormNodeLibToApiModel = (formNode: FormNodeLib): NodeLibAm => ({
  ...formNode,
  attributes: formNode.attributes.map((x) => x.value),
  selectedAttributePredefined: formNode.selectedAttributePredefined.map((x) =>
    mapFormSelectedAttributePredefinedLibToApiModel(x)
  ),
});

export const mapNodeLibCmToFormNodeLib = (nodeLibCm: NodeLibCm, mode?: NodeFormMode): FormNodeLib => ({
  ...mapNodeLibCmToNodeLibAm(nodeLibCm),
  parentId: mode === "clone" ? nodeLibCm.id : nodeLibCm.parentId,
  attributes: nodeLibCm.attributes.map((x) => ({ value: x })),
  selectedAttributePredefined: nodeLibCm.selectedAttributePredefined.map((x) => ({
    ...x,
    values: Object.keys(x.values).map((y) => ({ value: y })),
  })),
});

const mapNodeLibCmToNodeLibAm = (node: NodeLibCm): NodeLibAm => ({
  ...node,
  nodeTerminals: node.nodeTerminals.map((x) => ({
    ...x,
    terminalId: x.terminal.id,
  })),
});

export const createEmptyFormNodeLib = (): FormNodeLib => ({
  ...emptyNodeLibAm,
  attributes: [],
  selectedAttributePredefined: [],
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
  parentId: "",
  version: "1.0",
};
