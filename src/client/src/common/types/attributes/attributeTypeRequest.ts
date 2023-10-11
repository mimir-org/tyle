import { ProvenanceQualifier } from "./provenanceQualifier";
import { RangeQualifier } from "./rangeQualifier";
import { RegularityQualifier } from "./regularityQualifier";
import { ScopeQualifier } from "./scopeQualifier";
import { ValueConstraintRequest } from "./valueConstraintRequest";

export interface AttributeTypeRequest {
  name: string;
  description: string | undefined;
  predicateId: number | undefined;
  unitIds: number[];
  unitMinCount: number;
  unitMaxCount: number;
  provenanceQualifier: ProvenanceQualifier | undefined;
  rangeQualifier: RangeQualifier | undefined;
  regularityQualifier: RegularityQualifier | undefined;
  scopeQualifier: ScopeQualifier | undefined;
  valueConstraint: ValueConstraintRequest | undefined;
}
