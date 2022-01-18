import { TerminalTypeItem } from "../../../../../../../models";

// eslint-disable-next-line @typescript-eslint/ban-types
const OnDirectionChange = (item: number, defaultTerminal: TerminalTypeItem, onChange: Function) => {
  onChange("update", { ...defaultTerminal, connectorType: Number(item) });
};

export default OnDirectionChange;
