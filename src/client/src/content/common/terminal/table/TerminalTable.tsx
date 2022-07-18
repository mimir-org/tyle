import { Table, Tbody, Thead, Tr } from "../../../../complib/data-display";
import { TerminalItem } from "../../../types/TerminalItem";
import { TerminalTableAmount } from "./TerminalTableAmount";
import { TerminalTableAttributes } from "./TerminalTableAttributes";
import { TerminalTableDirection } from "./TerminalTableDirection";
import { TerminalTableHeader } from "./TerminalTableHeader";
import { TerminalTableIdentifier } from "./TerminalTableIdentifier";

/**
 * Components which lists terminals in a table and presents their most important features.
 *
 * @param terminals to show inside the table
 * @constructor
 */
export const TerminalTable = ({ terminals }: { terminals: TerminalItem[] }) => (
  <Table borders width={"100%"}>
    <Thead>
      <TerminalTableHeader />
    </Thead>
    <Tbody>
      {terminals.map((terminal, i) => (
        <Tr key={i + terminal.name}>
          <TerminalTableIdentifier {...terminal} />
          <TerminalTableDirection {...terminal} />
          <TerminalTableAmount {...terminal} />
          <TerminalTableAttributes {...terminal} />
        </Tr>
      ))}
    </Tbody>
  </Table>
);
