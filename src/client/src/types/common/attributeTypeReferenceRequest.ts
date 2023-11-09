import { HasCardinality } from "./hasCardinality";

export interface AttributeTypeReferenceRequest extends HasCardinality {
  attributeId: string;
}
