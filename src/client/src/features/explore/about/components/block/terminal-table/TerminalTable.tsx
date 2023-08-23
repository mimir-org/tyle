import { BlockTerminalItem } from "common/types/blockTerminalItem";
import { Table, Tbody, Thead, Tr } from "@mimirorg/component-library";
import { TerminalTableAmount } from "features/explore/about/components/block/terminal-table/TerminalTableAmount";
import { TerminalTableAttributes } from "features/explore/about/components/block/terminal-table/TerminalTableAttributes";
import { TerminalTableDirection } from "features/explore/about/components/block/terminal-table/TerminalTableDirection";
import { TerminalTableHeader } from "features/explore/about/components/block/terminal-table/TerminalTableHeader";
import { TerminalTableIdentifier } from "features/explore/about/components/block/terminal-table/TerminalTableIdentifier";

/**
 * Components which lists terminals in a table and presents their most important features.
 *
 * @param terminals to show inside the table
 * @constructor
 */
export const TerminalTable = ({ terminals }: { terminals: BlockTerminalItem[] }) => (
  <Table borders width={"100%"}>
    <Thead>
      <TerminalTableHeader />
    </Thead>
    <Tbody>
      {terminals.map((terminal) => (
        <Tr key={terminal.name + terminal.color + terminal.direction}>
          <TerminalTableIdentifier {...terminal} />
          <TerminalTableDirection {...terminal} />
          <TerminalTableAmount {...terminal} />
          <TerminalTableAttributes {...terminal} />
        </Tr>
      ))}
    </Tbody>
  </Table>
);
