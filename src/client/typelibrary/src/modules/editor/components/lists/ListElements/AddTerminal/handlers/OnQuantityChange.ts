import { TerminalTypeItem } from "../../../../../../../models";

// eslint-disable-next-line @typescript-eslint/ban-types
const OnQuantityChange = (item: number, defaultTerminal: TerminalTypeItem, onChange: Function) => {
  onChange("update", { ...defaultTerminal, number: item });
};

export default OnQuantityChange;
