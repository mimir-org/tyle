import { Table, Tbody, Thead, Tr } from "@mimirorg/component-library";
import { BlockTerminalItem } from "common/types/blockTerminalItem";
import TerminalTableAmount from "./TerminalTableAmount";
import TerminalTableAttributes from "./TerminalTableAttributes";
import TerminalTableDirection from "./TerminalTableDirection";
import TerminalTableHeader from "./TerminalTableHeader";
import TerminalTableIdentifier from "./TerminalTableIdentifier";

/**
 * Components which lists terminals in a table and presents their most important features.
 *
 * @param terminals to show inside the table
 * @constructor
 */
const TerminalTable = ({ terminals }: { terminals: BlockTerminalItem[] }) => (
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

export default TerminalTable;
