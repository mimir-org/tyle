import { ChangeEvent } from "react";
import { ConnectorDirection } from "../../../../models/tyle/enums/connectorDirection";

export const onTerminalAmountChange = (
  index: number,
  event: ChangeEvent<HTMLInputElement>,
  removeTerminal: (index: number) => void,
  callback: (event: ChangeEvent<HTMLInputElement>) => void
) => {
  const amount = parseInt(event.target.value);
  if (amount < 1) removeTerminal(index);
  callback(event);
};

export const connectorDirectionOptions = [
  { value: ConnectorDirection.Input, label: "Input" },
  { value: ConnectorDirection.Output, label: "Output" },
  { value: ConnectorDirection.Bidirectional, label: "Bidirectional" },
];
