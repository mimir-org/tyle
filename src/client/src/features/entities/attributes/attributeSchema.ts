import { object, string, number, array, boolean, InferType } from "yup";

export const attributeSchema = object({
  name: string().max(60).required(),
  companyId: number().min(1).required(),
  description: string().max(500).required(),
  attributeUnits: array().of(
    object({
      unitId: string().required(),
      isDefault: boolean().required(),
      description: string().max(500).required(),
    })
  ),
});

export type AttributeSchema = InferType<typeof attributeSchema>;
