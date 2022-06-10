import { ConnectorDirection } from "../enums/connectorDirection";
import { TerminalLibCm } from "./terminalLibCm";

export interface NodeTerminalLibCm {
  id: string;
  quantity: number;
  connectorDirection: ConnectorDirection;
  terminal: TerminalLibCm;
  kind: string;
}
