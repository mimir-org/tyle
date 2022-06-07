import { NodeTerminalLibAm } from "../../models/tyle/application/nodeTerminalLibAm";
import { NodeTerminalLibCm } from "../../models/tyle/client/nodeTerminalLibCm";

export const mapNodeTerminalLibCmToNodeTerminalLibAm = (nodeTerminal: NodeTerminalLibCm): NodeTerminalLibAm => ({
  ...nodeTerminal,
  terminalId: nodeTerminal.terminal.id,
});

export const mapNodeTerminalLibCmsToNodeTerminalLibAms = (nodeTerminals: NodeTerminalLibCm[]): NodeTerminalLibAm[] =>
  nodeTerminals.map((x) => mapNodeTerminalLibCmToNodeTerminalLibAm(x));
