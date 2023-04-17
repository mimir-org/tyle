import { AttributeLibAm, AttributeUnitLibAm, AttributeLibCm } from "@mimirorg/typelibrary-types";
import { ValueObject } from "../../types/valueObject";
import { UpdateUnitEntity } from "../../../../common/types/updateEntity";

export interface FormAttributeLib extends Omit<AttributeLibAm, "attributeUnits"> {
  attributeUnits: ValueObject<UpdateUnitEntity<AttributeUnitLibAm>>[];
}

export const mapFormAttributeLibToApiModel = (formAttribute: FormAttributeLib): AttributeLibAm => ({
  ...formAttribute,
  attributeUnits: formAttribute.attributeUnits.map((x) => x.value),
});

export const createEmptyFormAttributeLib = (): FormAttributeLib => ({
  ...emptyAttributeLib,
  attributeUnits: [],
});

export const mapAttributeLibCmToFormAttributeLib = (attribute: AttributeLibCm): FormAttributeLib => ({
  ...attribute,
  attributeUnits: attribute.attributeUnits.map((x) => ({ value: { unitId: x.unit.id, isDefault: x.isDefault } })),
  companyId: attribute.companyId,
  description: attribute.description,
  name: attribute.name,
  typeReference: attribute.typeReference,
});

const emptyAttributeLib: AttributeLibAm = {
  attributeUnits: [],
  name: "",
  typeReference: "",
  description: "",
  companyId: 0,
};
