import { Aspect } from "../common/aspect";
import { AttributeTypeReferenceRequest } from "../common/attributeTypeReferenceRequest";
import { TerminalTypeReferenceRequest } from "./terminalTypeReferenceRequest";

export interface BlockTypeRequest {
  name: string;
  description: string | null;
  classifierIds: number[];
  purposeId: number | null;
  notation: string | null;
  symbolId: number | null;
  aspect: Aspect | null;
  terminals: TerminalTypeReferenceRequest[];
  attributes: AttributeTypeReferenceRequest[];
}
