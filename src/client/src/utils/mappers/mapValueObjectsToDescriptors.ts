import { ValueObject } from "../../content/forms/types/valueObject";

export const mapValueObjectsToDescriptors = (valueObjects: ValueObject<string>[]) => {
  const descriptors: { [key: string]: string } = {};

  valueObjects.forEach((valueObject, index) => {
    descriptors[index] = valueObject.value;
  });

  return descriptors;
};
