import { NodeTerminalItem } from "common/types/nodeTerminalItem";
import { Table, Tbody, Thead, Tr } from "complib/data-display";
import { TerminalTableAmount } from "features/common/terminal/table/TerminalTableAmount";
import { TerminalTableAttributes } from "features/common/terminal/table/TerminalTableAttributes";
import { TerminalTableDirection } from "features/common/terminal/table/TerminalTableDirection";
import { TerminalTableHeader } from "features/common/terminal/table/TerminalTableHeader";
import { TerminalTableIdentifier } from "features/common/terminal/table/TerminalTableIdentifier";

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
