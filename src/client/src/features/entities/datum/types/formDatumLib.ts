import { QuantityDatumLibAm, QuantityDatumLibCm, QuantityDatumType, State } from "@mimirorg/typelibrary-types";

export const toDatumLibAm = (datum: QuantityDatumLibCm): QuantityDatumLibAm => ({
  ...datum,
  name: datum.name,
  typeReference: datum.typeReference,
  description: datum.description,
});

export const createEmptyDatum = (): QuantityDatumLibCm => ({
  id: "",
  iri: "",
  created: new Date(),
  state: State.Draft,
  kind: "",
  createdBy: "",
  name: "",
  typeReference: "",
  quantityDatumType: QuantityDatumType.QuantityDatumRangeSpecifying,
  description: "",
});