import { Aspect, AttributeLibAm, Discipline, Select } from "@mimirorg/typelibrary-types";

export const createEmptyAttributeLibAm = (): AttributeLibAm => ({
  name: "",
  aspect: Aspect.None,
  discipline: Discipline.None,
  select: Select.None,
  attributeQualifier: "",
  attributeSource: "",
  attributeCondition: "",
  attributeFormat: "",
  companyId: 0,
  typeReferences: [],
  selectValues: [],
  unitIdList: [],
  tags: [],
});
