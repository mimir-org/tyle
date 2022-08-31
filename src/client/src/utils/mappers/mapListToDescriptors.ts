export const mapListToDescriptors = <T>(list: T[]) => {
  const descriptors: { [key: string]: T } = {};

  list.forEach((value, index) => {
    descriptors[index] = value;
  });

  return descriptors;
};
