import { HasCardinality } from "../common/hasCardinality";
import { Direction } from "../terminals/direction";

export interface TerminalTypeReferenceRequest extends HasCardinality {
  direction: Direction;
  terminalId: string;
  connectionPointId: number | null;
}
