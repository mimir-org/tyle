import { NodeTerminalItem } from "common/types/nodeTerminalItem";
import { Table, Tbody, Thead, Tr } from "complib/data-display";
import { TerminalTableAmount } from "features/explore/about/components/node/terminal-table/TerminalTableAmount";
import { TerminalTableAttributes } from "features/explore/about/components/node/terminal-table/TerminalTableAttributes";
import { TerminalTableDirection } from "features/explore/about/components/node/terminal-table/TerminalTableDirection";
import { TerminalTableHeader } from "features/explore/about/components/node/terminal-table/TerminalTableHeader";
import { TerminalTableIdentifier } from "features/explore/about/components/node/terminal-table/TerminalTableIdentifier";

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
