import { ConnectorDirection, NodeTerminalLibAm } from "@mimirorg/typelibrary-types";

export const createEmptyNodeTerminalLibAm = (): NodeTerminalLibAm => ({
  terminalId: "",
  quantity: 1,
  connectorDirection: ConnectorDirection.Input,
});
