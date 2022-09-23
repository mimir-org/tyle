import { NodeLibAm, NodeLibCm } from "@mimirorg/typelibrary-types";
import { UpdateEntity } from "../../../../data/types/updateEntity";
import { createEmptyNodeLibAm } from "../../../../models/tyle/application/nodeLibAm";
import { mapNodeLibCmToNodeLibAm } from "../../../../utils/mappers";
import { ValueObject } from "../../types/valueObject";
import {
  FormSelectedAttributePredefinedLib,
  mapFormSelectedAttributePredefinedLibToApiModel,
} from "./formSelectedAttributePredefinedLib";
import { NodeFormMode } from "./nodeFormMode";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormNodeLib extends Omit<UpdateEntity<NodeLibAm>, "attributeIdList" | "selectedAttributePredefined"> {
  attributeIdList: ValueObject<string>[];
  selectedAttributePredefined: FormSelectedAttributePredefinedLib[];
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formNode client-only model
 */
export const mapFormNodeLibToApiModel = (formNode: FormNodeLib): UpdateEntity<NodeLibAm> => ({
  ...formNode,
  attributeIdList: formNode.attributeIdList.map((x) => x.value),
  selectedAttributePredefined: formNode.selectedAttributePredefined.map((x) =>
    mapFormSelectedAttributePredefinedLibToApiModel(x)
  ),
});

export const createEmptyFormNodeLib = (): FormNodeLib => ({
  ...createEmptyNodeLibAm(),
  id: "",
  attributeIdList: [],
  selectedAttributePredefined: [],
});

export const mapNodeLibCmToFormNodeLib = (nodeLibCm: NodeLibCm, mode?: NodeFormMode): FormNodeLib => ({
  ...mapNodeLibCmToNodeLibAm(nodeLibCm),
  id: nodeLibCm.id,
  parentId: mode === "clone" ? nodeLibCm.id : nodeLibCm.parentId,
  attributeIdList: nodeLibCm.attributes.map((x) => ({
    value: x.id,
  })),
  selectedAttributePredefined: nodeLibCm.selectedAttributePredefined.map((x) => ({
    ...x,
    values: Object.keys(x.values).map((y) => ({ value: y })),
  })),
});
