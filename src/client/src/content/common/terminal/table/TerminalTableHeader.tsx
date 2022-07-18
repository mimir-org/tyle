import { useTheme } from "styled-components";
import textResources from "../../../../assets/text/TextResources";
import { Th, Tr } from "../../../../complib/data-display";
import { Text } from "../../../../complib/text";
import { useMediaQuery } from "../../../../hooks/useMediaQuery";

export const TerminalTableHeader = () => {
  const theme = useTheme();
  const adjustAmountAlignment = useMediaQuery("screen and (min-width: 1500px)");
  const textColor = theme.tyle.color.sys.primary.base;

  return (
    <Tr>
      <Th>
        <Text as={"span"} color={textColor}>
          {textResources.TERMINAL_TABLE_NAME}
        </Text>
      </Th>
      <Th>
        <Text as={"span"} color={textColor}>
          {textResources.TERMINAL_TABLE_DIRECTION}
        </Text>
      </Th>
      <Th textAlign={adjustAmountAlignment ? "center" : "left"}>
        <Text as={"span"} color={textColor}>
          {textResources.TERMINAL_TABLE_AMOUNT}
        </Text>
      </Th>
      <Th>
        <Text as={"span"} color={textColor}>
          {textResources.TERMINAL_TABLE_ATTRIBUTES}
        </Text>
      </Th>
    </Tr>
  );
};