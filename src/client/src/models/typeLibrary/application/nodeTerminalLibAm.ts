import { ConnectorDirection } from "../enums/connectorDirection";

export interface NodeTerminalLibAm {
  terminalId: string;
  number: number;
  connectorDirection: ConnectorDirection;
  id: string;
}
