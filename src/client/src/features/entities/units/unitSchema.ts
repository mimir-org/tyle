import * as yup from "yup";

export const unitSchema = yup.object({
  id: yup.number().required(),
  name: yup.string().required(),
  symbol: yup.string(),
  description: yup.string(),
  iri: yup.string().url().required(),
  source: yup.number().required(),
});
