import { TerminalButton } from "common/components/terminal/TerminalButton";
import { NodeTerminalItem } from "common/types/nodeTerminalItem";
import { Td } from "complib/data-display";
import { Flexbox } from "complib/layouts";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

export const TerminalTableIdentifier = ({
  name,
  color,
  direction,
}: Pick<NodeTerminalItem, "name" | "color" | "direction">) => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "terminals" });

  return (
    <Td data-label={t("templates.terminal", { object: t("name").toLowerCase() })}>
      <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
        <TerminalButton variant={"small"} as={"div"} color={color} direction={direction} />
        {name}
      </Flexbox>
    </Td>
  );
};
