import { Aspect } from "../common/aspect";
import { AttributeTypeReferenceRequest } from "../common/attributeTypeReferenceRequest";
import { Direction } from "./direction";

export interface TerminalTypeRequest {
  name: string;
  description?: string;
  classifierIds: number[];
  purposeId?: number;
  notation?: string;
  symbol?: string;
  aspect?: Aspect;
  mediumId?: number;
  qualifier: Direction;
  attributes: AttributeTypeReferenceRequest[];
}
