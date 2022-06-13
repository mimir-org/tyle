import { UpdateEntity } from "../../../data/types/updateEntity";
import { createEmptyNodeLibAm, NodeLibAm } from "../../../models/tyle/application/nodeLibAm";
import { NodeLibCm } from "../../../models/tyle/client/nodeLibCm";
import { mapNodeLibCmToNodeLibAm } from "../../../utils/mappers";
import {
  FormSelectedAttributePredefinedLibAm,
  mapFormSelectedAttributePredefinedLibAmToApiModel,
} from "./formSelectedAttributePredefinedLibAm";
import { ValueObject } from "./valueObject";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormNodeLib extends Omit<UpdateEntity<NodeLibAm>, "attributeIdList" | "selectedAttributePredefined"> {
  attributeIdList: ValueObject<string>[];
  selectedAttributePredefined: FormSelectedAttributePredefinedLibAm[];
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formNode client-only model
 */
export const mapFormNodeLibAmToApiModel = (formNode: FormNodeLib): UpdateEntity<NodeLibAm> => ({
  ...formNode,
  attributeIdList: formNode.attributeIdList.map((x) => x.value),
  selectedAttributePredefined: formNode.selectedAttributePredefined.map((x) =>
    mapFormSelectedAttributePredefinedLibAmToApiModel(x)
  ),
});

export const createEmptyFormNodeLibAm = (): FormNodeLib => ({
  ...createEmptyNodeLibAm(),
  id: "",
  attributeIdList: [],
  selectedAttributePredefined: [],
});

export const mapNodeLibCmToFormNodeLibAm = (nodeLibCm: NodeLibCm): FormNodeLib => ({
  ...mapNodeLibCmToNodeLibAm(nodeLibCm),
  id: nodeLibCm.id,
  attributeIdList: nodeLibCm.attributes.map((x) => ({
    value: x.id,
  })),
  selectedAttributePredefined: nodeLibCm.selectedAttributePredefined.map((x) => ({
    ...x,
    values: Object.keys(x.values).map((y) => ({ value: y })),
  })),
});
