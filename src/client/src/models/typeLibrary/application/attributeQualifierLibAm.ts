export interface AttributeQualifierLibAm {
  name: string;
  description: string;
  updatedBy: string;
  updated: string | null;
  created: string;
  createdBy: string;
}

export const createEmptyAttributeQualifierLibAm = (): AttributeQualifierLibAm => {
  return {
    name: "",
    description: "",
    updatedBy: "",
    updated: null,
    created: "",
    createdBy: "",
  };
};
