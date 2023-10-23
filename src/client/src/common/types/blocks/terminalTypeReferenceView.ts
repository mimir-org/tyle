import { HasCardinality } from "../common/hasCardinality";
import { Direction } from "../terminals/direction";
import { TerminalView } from "../terminals/terminalView";

export interface TerminalTypeReferenceView extends HasCardinality {
  direction: Direction;
  terminal: TerminalView;
  minCount: number;
  maxCount?: number;
}
