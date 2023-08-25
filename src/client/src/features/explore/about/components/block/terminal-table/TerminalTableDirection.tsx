import { ArrowLeft, ArrowRight, ArrowsRightLeft } from "@styled-icons/heroicons-outline";
import { BlockTerminalItem } from "common/types/blockTerminalItem";
import { Flexbox, Td } from "@mimirorg/component-library";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

export const TerminalTableDirection = ({ direction }: Pick<BlockTerminalItem, "direction">) => {
  const theme = useTheme();
  const { t } = useTranslation("explore");
  const directionIconSize = 20;

  return (
    <Td data-label={t("about.terminals.templates.terminal", { object: t("about.terminals.direction").toLowerCase() })}>
      <Flexbox alignItems={"center"} gap={theme.mimirorg.spacing.base}>
        {direction === "Input" && <ArrowRight color={theme.mimirorg.color.primary.base} size={directionIconSize} />}
        {direction === "Output" && <ArrowLeft color={theme.mimirorg.color.primary.base} size={directionIconSize} />}
        {direction === "Bidirectional" && (
          <ArrowsRightLeft color={theme.mimirorg.color.primary.base} size={directionIconSize} />
        )}
        {direction}
      </Flexbox>
    </Td>
  );
};