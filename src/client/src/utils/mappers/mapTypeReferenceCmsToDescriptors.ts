import { TypeReferenceCm } from "@mimirorg/typelibrary-types";

export const mapTypeReferenceCmsToDescriptors = (references: TypeReferenceCm[]) => {
  const descriptors: { [key: string]: string } = {};

  references.forEach((reference, index) => {
    descriptors[index] = reference.name;
  });

  return descriptors;
};
