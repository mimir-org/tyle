import { Aspect } from "../enums/aspect";
import { Discipline } from "../enums/discipline";
import { Select } from "../enums/select";

export interface AttributeLibAm {
  name: string;
  aspect: Aspect;
  discipline: Discipline;
  select: Select;
  attributeQualifier: string;
  attributeSource: string;
  attributeCondition: string;
  attributeFormat: string;
  contentReferences: string[];
  parentId: string;
  selectValues: string[];
  unitIdList: string[];
  tags: Set<string>;
}
