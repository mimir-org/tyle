import { AttributeLibAm, AttributeLibCm, AttributeUnitLibAm } from "@mimirorg/typelibrary-types";
import { FormUnitHelper } from "../../units/types/FormUnitHelper";

export interface FormAttributeLib extends Omit<AttributeLibAm, "attributeUnits"> {
  attributeUnits: FormUnitHelper[];
}

export const fromFormAttributeLibToApiModel = (formAttribute: FormAttributeLib): AttributeLibAm => ({
  ...formAttribute,
  attributeUnits: formAttribute.attributeUnits.map((x: AttributeUnitLibAm) => ({
    unitId: x.unitId,
    isDefault: x.isDefault,
  })),
});

export const toFormAttributeLib = (attribute: AttributeLibCm): FormAttributeLib => ({
  ...attribute,
  attributeUnits: attribute.attributeUnits.map((x) => ({
    unitId: x.id,
    isDefault: x.isDefault,
    name: x.unit.name,
    description: x.unit.description,
    symbol: x.unit.symbol,
  })),
});

export const toAttributeLibAm = (attribute: AttributeLibCm): AttributeLibAm => ({
  ...attribute,
  attributeUnits: attribute.attributeUnits.map((x) => ({ unitId: x.unit.id, isDefault: x.isDefault })),
  description: attribute.description,
  name: attribute.name,
  typeReference: attribute.typeReference,
});

export const createEmptyAttribute = (): FormAttributeLib => ({
  attributeUnits: [],
  name: "",
  typeReference: "",
  description: "",
});
