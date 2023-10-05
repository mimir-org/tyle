import { Aspect } from "../common/aspect";
import { AttributeTypeReferenceRequest } from "../common/attributeTypeReferenceRequest";
import { Direction } from "./direction";

export interface TerminalTypeRequest {
  name: string;
  description: string | null;
  classifierIds: number[];
  purposeId: number | null;
  notation: string | null;
  symbol: string | null;
  aspect: Aspect | null;
  mediumId: number | null;
  qualifier: Direction;
  attributes: AttributeTypeReferenceRequest[];
}
