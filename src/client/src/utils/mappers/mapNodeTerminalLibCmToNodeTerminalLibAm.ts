import { NodeTerminalLibAm, NodeTerminalLibCm } from "@mimirorg/typelibrary-types";

export const mapNodeTerminalLibCmToNodeTerminalLibAm = (nodeTerminal: NodeTerminalLibCm): NodeTerminalLibAm => ({
  ...nodeTerminal,
  terminalId: nodeTerminal.terminal.id,
});

export const mapNodeTerminalLibCmsToNodeTerminalLibAms = (nodeTerminals: NodeTerminalLibCm[]): NodeTerminalLibAm[] =>
  nodeTerminals?.map((x) => mapNodeTerminalLibCmToNodeTerminalLibAm(x));
