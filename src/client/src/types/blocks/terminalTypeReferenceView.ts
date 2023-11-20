import { HasCardinality } from "../common/hasCardinality";
import { Direction } from "../terminals/direction";
import { TerminalView } from "../terminals/terminalView";
import { ConnectionPoint } from "./connectionPoint";

export interface TerminalTypeReferenceView extends HasCardinality {
  direction: Direction;
  terminal: TerminalView;
  connectionPoint: ConnectionPoint | null;
}
