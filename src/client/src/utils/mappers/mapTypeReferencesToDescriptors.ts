import { TypeReferenceAm, TypeReferenceCm } from "@mimirorg/typelibrary-types";

export const mapTypeReferencesToDescriptors = (references: TypeReferenceAm[] | TypeReferenceCm[]) => {
  const descriptors: { [key: string]: string } = {};

  references.forEach((reference, index) => {
    descriptors[index] = reference.name;
  });

  return descriptors;
};
