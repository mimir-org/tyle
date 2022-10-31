import { ConnectorDirection, NodeTerminalLibAm } from "@mimirorg/typelibrary-types";

export const onTerminalAmountChange = (
  fieldIndex: number,
  fieldValue: number,
  removeTerminal: (fieldIndex: number) => void,
  onChangeCallback: (value: number) => void
) => {
  if (fieldValue < 1) removeTerminal(fieldIndex);
  onChangeCallback(fieldValue);
};

export const createEmptyNodeTerminalLibAm = (): NodeTerminalLibAm => ({
  terminalId: "",
  quantity: 1,
  connectorDirection: ConnectorDirection.Input,
});
