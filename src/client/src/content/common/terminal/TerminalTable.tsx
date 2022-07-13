import { ArrowLeft, ArrowRight, SwitchHorizontal } from "@styled-icons/heroicons-outline";
import { useTheme } from "styled-components";
import textResources from "../../../assets/text/TextResources";
import { Table, Tbody, Td, Th, Thead, Tr } from "../../../complib/data-display";
import { Box, Flexbox } from "../../../complib/layouts";
import { Text } from "../../../complib/text";
import { useMediaQuery } from "../../../hooks/useMediaQuery";
import { TerminalItem } from "../../home/types/TerminalItem";
import { AttributeInfoButton } from "../attribute";
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
export const TerminalTable = ({ terminals }: TerminalTableProps) => {
  const theme = useTheme();
  const directionIconSize = 20;
  const adjustAmountAlignment = useMediaQuery("screen and (min-width: 1500px)");

  return (
    <Table borders width={"100%"}>
      <Thead>
        <Tr>
          <Th>
            <Text as={"span"} color={theme.tyle.color.sys.primary.base}>
              {textResources.TERMINAL_TABLE_NAME}
            </Text>
          </Th>
          <Th>
            <Text as={"span"} color={theme.tyle.color.sys.primary.base}>
              {textResources.TERMINAL_TABLE_DIRECTION}
            </Text>
          </Th>
          <Th textAlign={adjustAmountAlignment ? "center" : "left"}>
            <Text as={"span"} color={theme.tyle.color.sys.primary.base}>
              {textResources.TERMINAL_TABLE_AMOUNT}
            </Text>
          </Th>
          <Th>
            <Text as={"span"} color={theme.tyle.color.sys.primary.base}>
              {textResources.TERMINAL_TABLE_ATTRIBUTES}
            </Text>
          </Th>
        </Tr>
      </Thead>
      <Tbody>
        {terminals.map((terminal, i) => (
          <Tr key={i + terminal.name}>
            <Td data-label={textResources.TERMINAL_TABLE_NAME}>
              <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
                <TerminalButton variant={"small"} as={"div"} color={terminal.color} direction={terminal.direction} />
                {terminal.name}
              </Flexbox>
            </Td>
            <Td data-label={textResources.TERMINAL_TABLE_DIRECTION}>
              <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
                {terminal.direction === "Input" && <ArrowRight style={{ flexShrink: 0 }} size={directionIconSize} />}
                {terminal.direction === "Output" && <ArrowLeft style={{ flexShrink: 0 }} size={directionIconSize} />}
                {terminal.direction === "Bidirectional" && (
                  <SwitchHorizontal style={{ flexShrink: 0 }} size={directionIconSize} />
                )}
                {terminal.direction}
              </Flexbox>
            </Td>
            <Td data-label={textResources.TERMINAL_TABLE_AMOUNT} textAlign={adjustAmountAlignment ? "center" : "left"}>
              {terminal.amount}
            </Td>
            <Td data-label={textResources.TERMINAL_TABLE_ATTRIBUTES}>
              <Box display={"flex"} flexWrap={"wrap"} minWidth={"200px"} gap={theme.tyle.spacing.base}>
                {terminal.attributes?.map((a, index) => (
                  <AttributeInfoButton key={index} {...a} />
                ))}
              </Box>
            </Td>
          </Tr>
        ))}
      </Tbody>
    </Table>
  );
};
