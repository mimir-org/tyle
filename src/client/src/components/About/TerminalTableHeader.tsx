import { Th, Tr } from "components/Table";
import Text from "components/Text";
import { useTheme } from "styled-components";
import { useMediaQuery } from "usehooks-ts";

const TerminalTableHeader = () => {
  const theme = useTheme();
  const adjustAmountAlignment = useMediaQuery("screen and (min-width: 1500px)");
  const textColor = theme.tyle.color.primary.base;

  return (
    <Tr>
      <Th>
        <Text as={"span"} color={textColor}>
          Terminal name
        </Text>
      </Th>
      <Th>
        <Text as={"span"} color={textColor}>
          Terminal direction
        </Text>
      </Th>
      <Th textAlign={adjustAmountAlignment ? "center" : "left"}>
        <Text as={"span"} color={textColor}>
          Max amount
        </Text>
      </Th>
      <Th>
        <Text as={"span"} color={textColor}>
          Terminal attributes
        </Text>
      </Th>
    </Tr>
  );
};

export default TerminalTableHeader;
