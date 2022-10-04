import { AttributeLibAm, AttributeLibCm } from "@mimirorg/typelibrary-types";
import { createEmptyAttributeLibAm } from "../../../../models/tyle/application/attributeLibAm";
import { mapAttributeLibCmToAttributeLibAm } from "../../../../utils/mappers";
import { ValueObject } from "../../types/valueObject";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormAttributeLib extends Omit<AttributeLibAm, "unitIdList" | "selectValues"> {
  unitIdList: ValueObject<string>[];
  selectValues: ValueObject<string>[];
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formAttribute client-only model
 */
export const mapFormAttributeLibToApiModel = (formAttribute: FormAttributeLib): AttributeLibAm => ({
  ...formAttribute,
  unitIdList: formAttribute.unitIdList.map((x) => x.value),
  selectValues: formAttribute.selectValues.map((x) => x.value),
});

export const createEmptyFormAttributeLib = (): FormAttributeLib => ({
  ...createEmptyAttributeLibAm(),
  unitIdList: [],
  selectValues: [],
});

export const mapAttributeLibCmToFormAttributeLib = (attributeLibCm: AttributeLibCm): FormAttributeLib => ({
  ...mapAttributeLibCmToAttributeLibAm(attributeLibCm),
  unitIdList: attributeLibCm.units.map((x) => ({
    value: x.id,
  })),
  selectValues: attributeLibCm.selectValues.map((x) => ({
    value: x,
  })),
});
