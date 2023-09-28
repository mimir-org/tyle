import { AttributeGroupLibAm, AttributeGroupLibCm, State } from "@mimirorg/typelibrary-types";
import { ValueObject } from "features/entities/types/valueObject";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormAttributeGroupLib extends Omit<AttributeGroupLibAm, "attributes"> {
  attributes: ValueObject<string>[];
  state: State;
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formTerminal client-only model
 */
export const mapFormAttributeGroupLibToApiModel = (formAttributeGroup: FormAttributeGroupLib): AttributeGroupLibAm => ({
  ...formAttributeGroup,
  attributeIds: formAttributeGroup.attributes.map((x) => x.value),
});

export const mapAttributeGroupLibCmToFormAttributeGroupLib = (
  attributeGroupLibCm: AttributeGroupLibCm,
): FormAttributeGroupLib => ({
  ...attributeGroupLibCm,
  attributes: attributeGroupLibCm.attributes.map((x) => ({ value: x.id })),
  attributeIds: attributeGroupLibCm.attributes.map((x) => x.id),
});

export const createEmptyFormAttributeGroupLib = (): FormAttributeGroupLib => ({
  ...emptyTerminalLibAm,
  attributes: [],
  state: State.Draft,
});

const emptyTerminalLibAm: AttributeGroupLibAm = {
  name: "",
  description: "",
  attributeIds: [],
};

export const toFormAttributeGroupLib = (attributeGroup: AttributeGroupLibCm): FormAttributeGroupLib => {
  // const defaultUnit = attributeGroup.attributeUnits.find((x) => x.isDefault)?.unit;

  return {
    name: attributeGroup.name,
    description: attributeGroup.description,
    attributeIds: [],
    attributes: [],
    state: attributeGroup.state,
  };
};
