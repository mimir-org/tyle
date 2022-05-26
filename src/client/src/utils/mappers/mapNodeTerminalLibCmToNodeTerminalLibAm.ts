import { NodeTerminalLibAm } from "../../models/tyle/application/nodeTerminalLibAm";
import { NodeTerminalLibCm } from "../../models/tyle/client/nodeTerminalLibCm";

export const mapNodeTerminalLibCmToNodeTerminalLibAm = (terminal: NodeTerminalLibCm): NodeTerminalLibAm => ({
  ...terminal,
  terminalId: terminal.id,
});

export const mapNodeTerminalLibCmsToNodeTerminalLibAms = (terminals: NodeTerminalLibCm[]): NodeTerminalLibAm[] =>
  terminals.map((x) => mapNodeTerminalLibCmToNodeTerminalLibAm(x));
