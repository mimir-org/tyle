import { Flexbox, Text } from "@mimirorg/component-library";
import { BlockTerminalItem } from "common/types/blockTerminalItem";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import TerminalButton from "../TerminalButton";
import { MAXIMUM_TERMINAL_QUANTITY_VALUE } from "./blockTerminalQuantityRestrictions";

const TerminalDescription = ({ name, maxQuantity, color, direction }: Omit<BlockTerminalItem, "id">) => {
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

export default TerminalDescription;
