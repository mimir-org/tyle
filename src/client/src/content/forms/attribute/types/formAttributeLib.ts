import { AttributeLibAm, AttributeLibCm } from "@mimirorg/typelibrary-types";
import { UpdateEntity } from "../../../../data/types/updateEntity";
import { createEmptyAttributeLibAm } from "../../../../models/tyle/application/attributeLibAm";
import { mapAttributeLibCmToAttributeLibAm } from "../../../../utils/mappers";
import { ValueObject } from "../../types/valueObject";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormAttributeLib extends Omit<UpdateEntity<AttributeLibAm>, "unitIdList"> {
  unitIdList: ValueObject<string>[];
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formAttribute client-only model
 */
export const mapFormAttributeLibToApiModel = (formAttribute: FormAttributeLib): UpdateEntity<AttributeLibAm> => ({
  ...formAttribute,
  unitIdList: formAttribute.unitIdList.map((x) => x.value),
});

export const createEmptyFormAttributeLib = (): FormAttributeLib => ({
  ...createEmptyAttributeLibAm(),
  id: "",
  unitIdList: [],
});

export const mapAttributeLibCmToFormAttributeLib = (attributeLibCm: AttributeLibCm): FormAttributeLib => ({
  ...mapAttributeLibCmToAttributeLibAm(attributeLibCm),
  id: attributeLibCm.id,
  unitIdList: attributeLibCm.units.map((x) => ({
    value: x.id,
  })),
});
