import { ArrowLeft, ArrowRight, SwitchHorizontal } from "@styled-icons/heroicons-outline";
import { NodeTerminalItem } from "common/types/nodeTerminalItem";
import { Td } from "complib/data-display";
import { Flexbox } from "complib/layouts";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

export const TerminalTableDirection = ({ direction }: Pick<NodeTerminalItem, "direction">) => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "terminals" });
  const directionIconSize = 20;

  return (
    <Td data-label={t("templates.terminal", { object: t("direction").toLowerCase() })}>
      <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
        {direction === "Input" && <ArrowRight color={theme.tyle.color.sys.primary.base} size={directionIconSize} />}
        {direction === "Output" && <ArrowLeft color={theme.tyle.color.sys.primary.base} size={directionIconSize} />}
        {direction === "Bidirectional" && (
          <SwitchHorizontal color={theme.tyle.color.sys.primary.base} size={directionIconSize} />
        )}
        {direction}
      </Flexbox>
    </Td>
  );
};
