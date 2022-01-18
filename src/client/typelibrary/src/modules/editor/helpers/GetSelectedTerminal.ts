import { CreateLibraryType, TerminalType, TerminalTypeDict } from "../../../models";

const GetSelectedTerminal = (createLibraryType: CreateLibraryType, terminals: TerminalTypeDict): TerminalType => {
  let selectedTerminal: TerminalType;
  terminals.forEach((t) => {
    t.value.forEach((element) => {
      if (element.id === createLibraryType?.terminalTypeId) {
        selectedTerminal = element;
      }
    });
  });
  return selectedTerminal;
};

export default GetSelectedTerminal;
