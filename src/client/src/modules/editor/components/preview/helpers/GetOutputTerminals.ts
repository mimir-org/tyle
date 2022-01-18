import { ConnectorType, CreateLibraryType, TerminalType, TerminalTypeDict } from "../../../../../models";

const GetOutputTerminals = (createLibraryType: CreateLibraryType, terminals: TerminalTypeDict): TerminalType[] => {
  const terminalsArray: TerminalType[] = [];
  createLibraryType?.terminalTypes
    .filter((t) => ConnectorType[t.connectorType] === ConnectorType[ConnectorType.Output])
    .forEach((t) => {
      terminals.forEach((x) => {
        x.value.forEach((y) => {
          if (y.id === t.terminalTypeId) {
            terminalsArray.push(y);
          }
        });
      });
    });
  return terminalsArray;
};

export default GetOutputTerminals;
