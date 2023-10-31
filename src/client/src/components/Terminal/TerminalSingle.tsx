import { BlockTerminalItem } from "common/types/blockTerminalItem";
import { Flexbox, Text, Tooltip } from "@mimirorg/component-library";
import { TerminalButton, TerminalButtonProps } from "components/Terminal/TerminalButton";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { MAXIMUM_TERMINAL_QUANTITY_VALUE } from "../../common/utils/blockTerminalQuantityRestrictions";

/**
 * Component which shows a single terminal for a given block in addition to its name and amount in a tooltip.
 *
 * @param name
 * @param amount
 * @param color
 * @param direction
 * @param variant
 * @constructor
 */
export const TerminalSingle = ({
  name,
  maxQuantity,
  color,
  direction,
  variant,
}: BlockTerminalItem & Pick<TerminalButtonProps, "variant">) => {
  return (
    <Tooltip
      content={<TerminalDescription name={name} maxQuantity={maxQuantity} color={color} direction={direction} />}
    >
      <TerminalButton color={color} direction={direction} variant={variant} />
    </Tooltip>
  );
};

export const TerminalDescription = ({ name, maxQuantity, color, direction }: Omit<BlockTerminalItem, "id">) => {
  const theme = useTheme();
  const { t } = useTranslation("common");
  const shownQuantity = maxQuantity === MAXIMUM_TERMINAL_QUANTITY_VALUE ? t("terminal.infinite") : maxQuantity;

  return (
    <Flexbox alignItems={"center"} gap={theme.mimirorg.spacing.base}>
      <TerminalButton as={"div"} color={color} direction={direction} />
      <Text variant={"body-small"}>{`${name}`}</Text>
      <Text spacing={{ ml: "auto" }} variant={"body-small"}>{`x ${shownQuantity}`}</Text>
    </Flexbox>
  );
};
