import { object, string, number } from "yup";

export const unitSchema = object({
  name: string().max(60).required(),
  typeReference: string().required(),
  companyId: number().min(1).required(),
  symbol: string().required(),
  description: string().max(500).required(),
});
