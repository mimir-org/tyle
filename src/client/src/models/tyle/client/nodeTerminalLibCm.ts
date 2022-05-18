import { ConnectorDirection } from "../enums/connectorDirection";
import { TerminalLibCm } from "./terminalLibCm";

export interface NodeTerminalLibCm {
  id: string;
  number: number;
  connectorDirection: ConnectorDirection;
  terminal: TerminalLibCm;
  kind: string;
}
