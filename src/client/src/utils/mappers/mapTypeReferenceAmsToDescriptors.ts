import { TypeReferenceAm } from "@mimirorg/typelibrary-types";

export const mapTypeReferenceAmsToDescriptors = (references: TypeReferenceAm[]) => {
  const descriptors: { [key: string]: string } = {};

  references.forEach((reference, index) => {
    descriptors[index] = reference.name;
  });

  return descriptors;
};
