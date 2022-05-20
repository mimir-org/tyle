import { TerminalTableContainer, TerminalTableData, TerminalTableHeader } from "./TerminalTable.styled";
import { TerminalItem } from "../../../../types/TerminalItem";
import { AttributeSingle } from "../attribute/AttributeSingle";
import { Flexbox } from "../../../../../../complib/layouts";
import { useTheme } from "styled-components";
import { TerminalButton } from "./TerminalButton";

interface TerminalTableProps {
  terminals: TerminalItem[];
}

/**
 * Components which lists terminals in a table and presents their most important features.
 *
 * @param terminals to show inside the table
 * @constructor
 */
export const TerminalTable = ({ terminals }: TerminalTableProps) => (
  <TerminalTableContainer>
    <thead>
      <TerminalTableHeaders />
    </thead>
    <tbody>
      {terminals.map((x, i) => (
        <TerminalTableRow key={i + x.name} {...x} />
      ))}
    </tbody>
  </TerminalTableContainer>
);

const TerminalTableHeaders = () => (
  <tr>
    <TerminalTableHeader>Name</TerminalTableHeader>
    <TerminalTableHeader>Direction</TerminalTableHeader>
    <TerminalTableHeader>Amount</TerminalTableHeader>
    <TerminalTableHeader>Attributes</TerminalTableHeader>
  </tr>
);

const TerminalTableRow = ({ name, amount, color, direction, attributes }: TerminalItem) => {
  const theme = useTheme();

  return (
    <tr>
      <TerminalTableData>
        <Flexbox alignItems={"center"} gap={theme.tyle.spacing.xs}>
          <TerminalButton as={"div"} color={color} variant={direction} />
          {name}
        </Flexbox>
      </TerminalTableData>
      <TerminalTableData>{direction}</TerminalTableData>
      <TerminalTableData>{amount}</TerminalTableData>
      <TerminalTableData>
        <Flexbox flexWrap={"wrap"} gap={theme.tyle.spacing.xs}>
          {attributes && attributes.map((a, i) => <AttributeSingle key={i} variant={"small"} {...a} />)}
        </Flexbox>
      </TerminalTableData>
    </tr>
  );
};
