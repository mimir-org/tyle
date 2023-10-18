import { Aspect } from "../common/aspect";
import { AttributeTypeReferenceRequest } from "../common/attributeTypeReferenceRequest";
import { TerminalTypeReferenceRequest } from "./terminalTypeReferenceRequest";

export interface BlockTypeRequest {
  name: string;
  description?: string;
  classifierIds: number[];
  purposeId?: number;
  notation?: string;
  symbol?: string;
  aspect: Aspect;
  terminals: TerminalTypeReferenceRequest[];
  attributes: AttributeTypeReferenceRequest[];
}
