import { ArrowLeft, ArrowRight, SwitchHorizontal } from "@styled-icons/heroicons-outline";
import { useTheme } from "styled-components";
import textResources from "../../../../../../assets/text/TextResources";
import { Flexbox } from "../../../../../../complib/layouts";
import { TerminalItem } from "../../../../types/TerminalItem";
import { AttributeInfoButton } from "../attribute/AttributeInfoButton";
import { TerminalButton } from "./TerminalButton";
import { TerminalTableContainer, TerminalTableData, TerminalTableHeader } from "./TerminalTable.styled";

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
      <tr>
        <TerminalTableHeader textAlign={"left"}>{textResources.TERMINAL_TABLE_NAME}</TerminalTableHeader>
        <TerminalTableHeader textAlign={"left"}>{textResources.TERMINAL_TABLE_DIRECTION}</TerminalTableHeader>
        <TerminalTableHeader textAlign={"center"}>{textResources.TERMINAL_TABLE_AMOUNT}</TerminalTableHeader>
        <TerminalTableHeader textAlign={"left"}>{textResources.TERMINAL_TABLE_ATTRIBUTES}</TerminalTableHeader>
      </tr>
    </thead>
    <tbody>
      {terminals.map((x, i) => (
        <TerminalTableRow key={i + x.name} {...x} />
      ))}
    </tbody>
  </TerminalTableContainer>
);

const TerminalTableRow = ({ name, amount, color, direction, attributes }: TerminalItem) => {
  const theme = useTheme();
  const directionIconSize = 20;

  return (
    <tr>
      <TerminalTableData textAlign={"left"}>
        <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
          <TerminalButton variant={"small"} as={"div"} color={color} direction={direction} />
          {name}
        </Flexbox>
      </TerminalTableData>
      <TerminalTableData textAlign={"left"}>
        <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
          {direction === "Input" && <ArrowRight size={directionIconSize} />}
          {direction === "Output" && <ArrowLeft size={directionIconSize} />}
          {direction === "Bidirectional" && <SwitchHorizontal size={directionIconSize} />}
          {direction}
        </Flexbox>
      </TerminalTableData>
      <TerminalTableData textAlign={"center"}>{amount}</TerminalTableData>
      <TerminalTableData textAlign={"left"}>
        <Flexbox flexWrap={"wrap"} gap={theme.tyle.spacing.base}>
          {attributes && attributes.map((a, i) => <AttributeInfoButton key={i} {...a} />)}
        </Flexbox>
      </TerminalTableData>
    </tr>
  );
};
