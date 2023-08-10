import { AspectObjectTerminalItem } from "common/types/aspectObjectTerminalItem";
import { Flexbox, Text, Tooltip } from "@mimirorg/component-library";
import { TerminalButton, TerminalButtonProps } from "features/common/terminal/TerminalButton";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { MAXIMUM_TERMINAL_QUANTITY_VALUE } from "../../../common/utils/aspectObjectTerminalQuantityRestrictions";

/**
 * Component which shows a single terminal for a given aspect object in addition to its name and amount in a tooltip.
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
}: AspectObjectTerminalItem & Pick<TerminalButtonProps, "variant">) => {
  return (
    <Tooltip
      content={<TerminalDescription name={name} maxQuantity={maxQuantity} color={color} direction={direction} />}
    >
      <TerminalButton color={color} direction={direction} variant={variant} />
    </Tooltip>
  );
};

export const TerminalDescription = ({ name, maxQuantity, color, direction }: Omit<AspectObjectTerminalItem, "id">) => {
  const theme = useTheme();
  const { t } = useTranslation("common");
  const shownQuantity = maxQuantity === MAXIMUM_TERMINAL_QUANTITY_VALUE ? t("terminal.infinite") : maxQuantity;

  return (
    <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
      <TerminalButton as={"div"} color={color} direction={direction} />
      <Text variant={"body-small"}>{`${name}`}</Text>
      <Text ml={"auto"} variant={"body-small"}>{`x ${shownQuantity}`}</Text>
    </Flexbox>
  );
};
