import { Aspect } from "../enums/aspect";
import { Discipline } from "../enums/discipline";
import { Select } from "../enums/select";
import { UnitLibCm } from "./unitLibCm";

export interface AttributeLibCm {
  id: string;
  parentName: string;
  parentIri: string;
  name: string;
  iri: string;
  contentReferences: string[];
  attributeQualifier: string;
  attributeSource: string;
  attributeCondition: string;
  attributeFormat: string;
  aspect: Aspect;
  discipline: Discipline;
  tags: Set<string>;
  select: Select;
  selectValues: string[];
  units: UnitLibCm[];
  description: string;
  created: string;
  createdBy: string;
  kind: string;
}
