import { TerminalType, TerminalTypeItem } from "../../../../../../../models";

// eslint-disable-next-line @typescript-eslint/ban-types
const OnTerminalIdChange = (item: TerminalType, defaultTerminal: TerminalTypeItem, onChange: Function) => {
  onChange("update", { ...defaultTerminal, terminalTypeId: item.id });
};

export default OnTerminalIdChange;
