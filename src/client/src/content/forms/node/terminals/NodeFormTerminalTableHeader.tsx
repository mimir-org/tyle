import { useTheme } from "styled-components";
import textResources from "../../../../assets/text/TextResources";
import { Th, Tr } from "../../../../complib/data-display";
import { Text } from "../../../../complib/text";
import { useMediaQuery } from "../../../../hooks/useMediaQuery";

export const NodeFormTerminalTableHeader = () => {
  const theme = useTheme();
  const adjustAttributesPadding = useMediaQuery("screen and (min-width: 1500px)");

  return (
    <Tr>
      <Th>
        <Text as={"span"} variant={"title-medium"}>
          {textResources.TERMINAL_TABLE_NAME}
        </Text>
      </Th>
      <Th>
        <Text as={"span"} variant={"title-medium"}>
          {textResources.TERMINAL_TABLE_AMOUNT}
        </Text>
      </Th>
      <Th>
        <Text as={"span"} variant={"title-medium"}>
          {textResources.TERMINAL_TABLE_DIRECTION}
        </Text>
      </Th>
      <Th>
        <Text as={"span"} variant={"title-medium"} pl={adjustAttributesPadding ? theme.tyle.spacing.xxxl : undefined}>
          {textResources.TERMINAL_TABLE_ATTRIBUTES}
        </Text>
      </Th>
    </Tr>
  );
};
