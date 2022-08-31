import { UnitLibCm } from "@mimirorg/typelibrary-types";

export const mapUnitLibCmsToDescriptors = (units: UnitLibCm[]) => {
  const descriptors: { [key: string]: string } = {};

  units.forEach((unit, index) => {
    descriptors[index] = unit.name;
  });

  return descriptors;
};
