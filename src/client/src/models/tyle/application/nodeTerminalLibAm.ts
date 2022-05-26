import { ConnectorDirection } from "../enums/connectorDirection";

export interface NodeTerminalLibAm {
  terminalId: string;
  number: number;
  connectorDirection: ConnectorDirection;
}

export const createEmptyNodeTerminalLibAm = (): NodeTerminalLibAm => ({
  terminalId: "",
  number: 1,
  connectorDirection: ConnectorDirection.Input,
});
