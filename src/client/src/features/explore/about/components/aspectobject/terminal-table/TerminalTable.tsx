import { AspectObjectTerminalItem } from "common/types/aspectObjectTerminalItem";
import { Table, Tbody, Thead, Tr } from "complib/data-display";
import { TerminalTableAmount } from "features/explore/about/components/aspectobject/terminal-table/TerminalTableAmount";
import { TerminalTableAttributes } from "features/explore/about/components/aspectobject/terminal-table/TerminalTableAttributes";
import { TerminalTableDirection } from "features/explore/about/components/aspectobject/terminal-table/TerminalTableDirection";
import { TerminalTableHeader } from "features/explore/about/components/aspectobject/terminal-table/TerminalTableHeader";
import { TerminalTableIdentifier } from "features/explore/about/components/aspectobject/terminal-table/TerminalTableIdentifier";

/**
 * Components which lists terminals in a table and presents their most important features.
 *
 * @param terminals to show inside the table
 * @constructor
 */
export const TerminalTable = ({ terminals }: { terminals: AspectObjectTerminalItem[] }) => (
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
