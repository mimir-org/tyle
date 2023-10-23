import { HasCardinality } from "../common/hasCardinality";
import { Direction } from "../terminals/direction";

export interface TerminalTypeReferenceRequest extends HasCardinality {
  direction: Direction;
  terminalId: string;
  maxCount?: number;
  minCount: number;
}
