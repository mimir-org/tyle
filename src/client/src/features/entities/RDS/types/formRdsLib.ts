import { RdsLibCm, State, RdsLibAm } from "@mimirorg/typelibrary-types";

export const toRdsLibAm = (datum: RdsLibCm): RdsLibAm => ({
  ...datum,
  name: datum.name,
  typeReference: datum.typeReference,
  description: datum.description,
});

export const createEmptyRds = (): RdsLibCm => ({
  name: "",
  id: "",
  iri: "",
  rdsCode: "",
  created: new Date(),
  createdBy: "",
  state: State.Draft,
  description: "",
  typeReference: "",
  categoryId: "",
  categoryName: "",
  kind: "",
});

export const toFormRdsLib = (rds: RdsLibCm): RdsLibCm => ({
  ...rds,
  name: rds.name,
  typeReference: rds.typeReference,
  description: rds.description,
});
