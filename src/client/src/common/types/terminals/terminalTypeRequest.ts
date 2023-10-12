import { Aspect } from "../common/aspect";
import { AttributeTypeReferenceRequest } from "../common/attributeTypeReferenceRequest";
import { Direction } from "./direction";

export interface TerminalTypeRequest {
  name: string;
  description: string | undefined;
  classifierIds: number[];
  purposeId: number | undefined;
  notation: string | undefined;
  symbol: string | undefined;
  aspect: Aspect | undefined;
  mediumId: number | undefined;
  qualifier: Direction;
  attributes: AttributeTypeReferenceRequest[];
}
