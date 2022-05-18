export interface AttributeQualifierLibAm {
  name: string;
  contentReferences: string[];
  description: string;
}

export const createEmptyAttributeQualifierLibAm = (): AttributeQualifierLibAm => {
  return {
    name: "",
    contentReferences: [],
    description: "",
  };
};
