import { ChangeEvent } from "react";

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
