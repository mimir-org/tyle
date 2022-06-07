import { createEmptyNodeLibAm, NodeLibAm } from "../../../models/tyle/application/nodeLibAm";
import { ValueObject } from "./valueObject";
import {
  FormSelectedAttributePredefinedLibAm,
  mapFormSelectedAttributePredefinedLibAmToApiModel,
} from "./formSelectedAttributePredefinedLibAm";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormNodeLib extends Omit<NodeLibAm, "attributeIdList" | "selectedAttributePredefined"> {
  attributeIdList: ValueObject<string>[];
  selectedAttributePredefined: FormSelectedAttributePredefinedLibAm[];
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formNode client-only model
 */
export const mapFormNodeLibAmToApiModel = (formNode: FormNodeLib): NodeLibAm => ({
  ...formNode,
  attributeIdList: formNode.attributeIdList.map((x) => x.value),
  selectedAttributePredefined: formNode.selectedAttributePredefined.map((x) =>
    mapFormSelectedAttributePredefinedLibAmToApiModel(x)
  ),
});

export const createEmptyFormNodeLibAm = (): FormNodeLib => ({
  ...createEmptyNodeLibAm(),
  attributeIdList: [],
  selectedAttributePredefined: [],
});
