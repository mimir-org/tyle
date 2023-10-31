import { BlockTerminalItem } from "common/types/blockTerminalItem";
import { Table, Tbody, Thead, Tr } from "@mimirorg/component-library";
import { TerminalTableAmount } from "components/About/TerminalTableAmount";
import { TerminalTableAttributes } from "components/About/TerminalTableAttributes";
import { TerminalTableDirection } from "components/About/TerminalTableDirection";
import { TerminalTableHeader } from "components/About/TerminalTableHeader";
import { TerminalTableIdentifier } from "components/About/TerminalTableIdentifier";

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
