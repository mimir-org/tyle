export const onTerminalAmountChange = (
  fieldIndex: number,
  fieldValue: number,
  removeTerminal: (fieldIndex: number) => void,
  onChangeCallback: (value: number) => void
) => {
  if (fieldValue < 1) removeTerminal(fieldIndex);
  onChangeCallback(fieldValue);
};
