import { Flexbox, Td } from "@mimirorg/component-library";
import TerminalButton from "components/TerminalButton";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { BlockTerminalItem } from "types/blockTerminalItem";

const TerminalTableIdentifier = ({
  name,
  color,
  direction,
}: Pick<BlockTerminalItem, "name" | "color" | "direction">) => {
  const theme = useTheme();
  const { t } = useTranslation("explore");

  return (
    <Td data-label={t("about.terminals.templates.terminal", { object: t("about.terminals.name").toLowerCase() })}>
      <Flexbox alignItems={"center"} gap={theme.mimirorg.spacing.base}>
        <TerminalButton variant={"small"} as={"div"} color={color} direction={direction} />
        {name}
      </Flexbox>
    </Td>
  );
};

export default TerminalTableIdentifier;
