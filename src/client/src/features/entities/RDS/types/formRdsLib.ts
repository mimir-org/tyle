import { RdsLibCm, RdsLibAm } from "@mimirorg/typelibrary-types";

export const toRdsLibAm = (datum: RdsLibCm): RdsLibAm => ({
  ...datum,
  name: datum.name,
  typeReference: datum.typeReference,
  description: datum.description,
});

export const createEmptyRds = (): RdsLibAm => ({
  name: "",
  rdsCode: "",
  description: "",
  typeReference: "",
  categoryId: "",
});

export const toFormRdsLib = (rds: RdsLibCm): RdsLibCm => ({
  ...rds,
  name: rds.name,
  typeReference: rds.typeReference,
  description: rds.description,
});
