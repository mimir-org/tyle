import { AttributeView } from "../attributes/attributeView";
import { HasCardinality } from "./hasCardinality";

export interface AttributeTypeReferenceView extends HasCardinality {
    attribute: AttributeView;
}