import { ProvenanceQualifier } from "./provenanceQualifier";
import { RangeQualifier } from "./rangeQualifier";
import { RegularityQualifier } from "./regularityQualifier";
import { ScopeQualifier } from "./scopeQualifier";
import { ValueConstraintRequest } from "./valueConstraintRequest";

export interface AttributeTypeRequest {
  name: string;
  description: string | null;
  predicateId: number | null;
  unitIds: number[];
  unitMinCount: number;
  unitMaxCount: number;
  provenanceQualifier: ProvenanceQualifier | null;
  rangeQualifier: RangeQualifier | null;
  regularityQualifier: RegularityQualifier | null;
  scopeQualifier: ScopeQualifier | null;
  valueConstraint: ValueConstraintRequest | null;
}
