import { TerminalLibCm } from "@mimirorg/typelibrary-types";
import { TerminalItem } from "../../types/terminalItem";
import { sortInfoItems } from "../sorters";
import { mapAttributeLibCmsToInfoItems } from "./mapAttributeLibCmToInfoItem";

export const mapTerminalLibCmToTerminalItem = (terminal: TerminalLibCm): TerminalItem => ({
  id: terminal.id,
  name: terminal.name,
  description: terminal.description,
  color: terminal.color,
  attributes: sortInfoItems(mapAttributeLibCmsToInfoItems(terminal.attributes)),
  tokens: [terminal.createdBy, terminal.version, terminal.companyName],
  kind: "TerminalItem",
});
