import { TerminalTableAmount } from "common/components/terminal/table/TerminalTableAmount";
import { TerminalTableAttributes } from "common/components/terminal/table/TerminalTableAttributes";
import { TerminalTableDirection } from "common/components/terminal/table/TerminalTableDirection";
import { TerminalTableHeader } from "common/components/terminal/table/TerminalTableHeader";
import { TerminalTableIdentifier } from "common/components/terminal/table/TerminalTableIdentifier";
import { NodeTerminalItem } from "common/types/nodeTerminalItem";
import { Table, Tbody, Thead, Tr } from "complib/data-display";

/**
 * Components which lists terminals in a table and presents their most important features.
 *
 * @param terminals to show inside the table
 * @constructor
 */
export const TerminalTable = ({ terminals }: { terminals: NodeTerminalItem[] }) => (
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
