import { ConnectorDirection } from "../enums/connectorDirection";

export interface NodeTerminalLibAm {
  terminalId: string;
  quantity: number;
  connectorDirection: ConnectorDirection;
}

export const createEmptyNodeTerminalLibAm = (): NodeTerminalLibAm => ({
  terminalId: "",
  quantity: 1,
  connectorDirection: ConnectorDirection.Input,
});
