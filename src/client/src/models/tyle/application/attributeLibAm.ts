import { Aspect, AttributeLibAm, Discipline, Select } from "@mimirorg/typelibrary-types";

export const createEmptyAttributeLibAm = (): AttributeLibAm => ({
  name: "",
  aspect: Aspect.None,
  discipline: Discipline.None,
  select: Select.None,
  description: "",
  quantityDatumSpecifiedScope: "",
  quantityDatumSpecifiedProvenance: "",
  quantityDatumRangeSpecifying: "",
  quantityDatumRegularitySpecified: "",
  companyId: 0,
  typeReferences: [],
  selectValues: [],
  unitIdList: [],
  version: "1.0",
});
