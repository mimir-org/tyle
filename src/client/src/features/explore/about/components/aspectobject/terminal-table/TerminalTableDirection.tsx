import { ArrowLeft, ArrowRight, ArrowsRightLeft } from "@styled-icons/heroicons-outline";
import { AspectObjectTerminalItem } from "common/types/aspectObjectTerminalItem";
import { Td } from "complib/data-display";
import { Flexbox } from "@mimirorg/component-library";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

export const TerminalTableDirection = ({ direction }: Pick<AspectObjectTerminalItem, "direction">) => {
  const theme = useTheme();
  const { t } = useTranslation("explore");
  const directionIconSize = 20;

  return (
    <Td data-label={t("about.terminals.templates.terminal", { object: t("about.terminals.direction").toLowerCase() })}>
      <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
        {direction === "Input" && <ArrowRight color={theme.tyle.color.sys.primary.base} size={directionIconSize} />}
        {direction === "Output" && <ArrowLeft color={theme.tyle.color.sys.primary.base} size={directionIconSize} />}
        {direction === "Bidirectional" && (
          <ArrowsRightLeft color={theme.tyle.color.sys.primary.base} size={directionIconSize} />
        )}
        {direction}
      </Flexbox>
    </Td>
  );
};
