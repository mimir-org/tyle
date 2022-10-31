import { TerminalLibCm } from "@mimirorg/typelibrary-types";
import { TerminalItem } from "common/types/terminalItem";
import { mapAttributeLibCmsToInfoItems } from "common/utils/mappers/mapAttributeLibCmToInfoItem";
import { sortInfoItems } from "common/utils/sorters";

export const mapTerminalLibCmToTerminalItem = (terminal: TerminalLibCm): TerminalItem => ({
  id: terminal.id,
  name: terminal.name,
  description: terminal.description,
  color: terminal.color,
  attributes: sortInfoItems(mapAttributeLibCmsToInfoItems(terminal.attributes)),
  tokens: [terminal.version, terminal.companyName],
  kind: "TerminalItem",
});
