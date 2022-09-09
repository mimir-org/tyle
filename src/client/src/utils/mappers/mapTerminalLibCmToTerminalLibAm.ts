import { TerminalLibAm, TerminalLibCm } from "@mimirorg/typelibrary-types";

export const mapTerminalLibCmToTerminalLibAm = (terminal: TerminalLibCm): TerminalLibAm => ({
  ...terminal,
  attributeIdList: terminal.attributes.map((x) => x.id),
  parentId: terminal.parentId,
});
