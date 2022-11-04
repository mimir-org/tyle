export const onTerminalAmountChange = (fieldValue: number, onChangeCallback: (value: number) => void) => {
  if (fieldValue > 0) onChangeCallback(fieldValue);
};
