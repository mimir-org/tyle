import { Aspect, AttributeLibAm, AttributeType, Discipline, Select } from "@mimirorg/typelibrary-types";

export const createEmptyAttributeLibAm = (): AttributeLibAm => ({
  name: "",
  aspect: Aspect.None,
  discipline: Discipline.None,
  select: Select.None,
  attributeQualifier: "",
  attributeSource: "",
  attributeCondition: "",
  attributeFormat: "",
  attributeType: AttributeType.Normal,
  contentReferences: [],
  parentId: "",
  selectValues: [],
  unitIdList: [],
  tags: [],
});
