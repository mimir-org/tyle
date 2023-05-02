import { object, string, number, array, boolean, InferType } from "yup";

export const attributeSchema = object({
  name: string().max(60).required(),
  companyId: number().min(1).required(),
  description: string().max(500).required(),
  attributeUnits: array()
    .of(
      object({
        unitId: string().required(),
        isDefault: boolean().required(),
        description: string().max(500).required(),
      })
    )
    .test("At least one default", "You need at least one default value", (attributeUnit) => {
      return attributeUnit ? attributeUnit.some((attributeUnit) => attributeUnit.isDefault) : false;
    })
    .required(),
});

export type AttributeSchema = InferType<typeof attributeSchema>;
