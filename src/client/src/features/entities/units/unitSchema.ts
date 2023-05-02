import { object, string } from "yup";

export const unitSchema = object({
  name: string().max(60).required(),
  typeReference: string().required(),
  symbol: string().required(),
  description: string().max(500).required(),
});
