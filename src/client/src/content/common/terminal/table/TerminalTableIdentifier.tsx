import { useTheme } from "styled-components";
import textResources from "../../../../assets/text/TextResources";
import { Td } from "../../../../complib/data-display";
import { Flexbox } from "../../../../complib/layouts";
import { TerminalItem } from "../../../types/TerminalItem";
import { TerminalButton } from "../TerminalButton";

export const TerminalTableIdentifier = ({
  name,
  color,
  direction,
}: Pick<TerminalItem, "name" | "color" | "direction">) => {
  const theme = useTheme();

  return (
    <Td data-label={textResources.TERMINAL_TABLE_NAME}>
      <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
        <TerminalButton variant={"small"} as={"div"} color={color} direction={direction} />
        {name}
      </Flexbox>
    </Td>
  );
};
