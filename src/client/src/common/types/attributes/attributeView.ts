import { ImfType } from "../common/imfType";
import { ProvenanceQualifier } from "./provenanceQualifier";
import { RangeQualifier } from "./rangeQualifier";
import { RdlPredicate } from "./rdlPredicate";
import { RdlUnit } from "./rdlUnit";
import { RegularityQualifier } from "./regularityQualifier";
import { ScopeQualifier } from "./scopeQualifier";
import { ValueConstraintView } from "./valueConstraintView";

export interface AttributeView extends ImfType {
  predicate: RdlPredicate | undefined;
  units: RdlUnit[];
  unitMinCount: number;
  unitMaxCount: number;
  provenanceQualifier: ProvenanceQualifier | undefined;
  rangeQualifier: RangeQualifier | undefined;
  regularityQualifier: RegularityQualifier | undefined;
  scopeQualifier: ScopeQualifier | undefined;
  valueConstraint: ValueConstraintView | undefined;
}
