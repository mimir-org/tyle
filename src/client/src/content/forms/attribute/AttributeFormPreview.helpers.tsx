import { TypeReferenceCm } from "@mimirorg/typelibrary-types";
import { ValueObject } from "../types/valueObject";

export const getDescriptorsFromValueObjects = (valueObjects: ValueObject<string>[]) => {
  const valueObjectDescriptors: { [key: string]: string } = {};

  valueObjects.forEach((valueObject, index) => {
    valueObjectDescriptors[index] = valueObject.value;
  });

  return valueObjectDescriptors;
};

export const getDescriptorsFromTypeReferences = (references: TypeReferenceCm[]) => {
  const valueObjectDescriptors: { [key: string]: string } = {};

  references.forEach((reference, index) => {
    valueObjectDescriptors[index] = reference.name;
  });

  return valueObjectDescriptors;
};
