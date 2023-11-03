import { Box, Divider, Flexbox, Popover, Text, VisuallyHidden } from "@mimirorg/component-library";
import TerminalButton from "components/TerminalButton";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { BlockTerminalItem } from "types/blockTerminalItem";
import TerminalDescription from "./TerminalDescription";
import { MAXIMUM_TERMINAL_QUANTITY_VALUE } from "./blockTerminalQuantityRestrictions";

interface TerminalCollectionProps {
  terminals: BlockTerminalItem[];
  placement?: "left" | "right";
}

/**
 * Displays multiple terminals in a popover which is shown when clicking on this component.
 *
 * @param terminals to show inside popover
 * @param variant decides which side the popover should appear
 * @constructor
 */
const TerminalCollection = ({ terminals, placement }: TerminalCollectionProps) => {
  const theme = useTheme();
  const { t } = useTranslation("common");

  return (
    <Popover placement={placement} content={<TerminalCollectionDescription terminals={terminals} />}>
      <TerminalButton variant={"large"} color={theme.mimirorg.color.reference.primary["40"]}>
        <VisuallyHidden>{t("terminal.summary.open")}</VisuallyHidden>
      </TerminalButton>
    </Popover>
  );
};

export default TerminalCollection;

interface TerminalCollectionDescriptionProps {
  terminals: BlockTerminalItem[];
}

const TerminalCollectionDescription = ({ terminals }: TerminalCollectionDescriptionProps) => {
  const theme = useTheme();
  const { t } = useTranslation("common");
  const totalTerminalAmount = terminals.reduce((sum, terminal) => sum + (terminal.maxQuantity ?? 0), 0);
  const shownTerminalAmount =
    totalTerminalAmount >= MAXIMUM_TERMINAL_QUANTITY_VALUE ? t("terminal.infinite") : totalTerminalAmount;

  return (
    <Box display={"flex"} gap={theme.mimirorg.spacing.l} flexDirection={"column"} maxWidth={"250px"}>
      <Text variant={"title-small"}>{t("terminal.summary.title")}</Text>
      <Box
        display={"flex"}
        gap={theme.mimirorg.spacing.l}
        flexDirection={"column"}
        maxHeight={"250px"}
        overflow={"auto"}
      >
        {terminals.map((x) => (
          <TerminalDescription
            key={x.name + x.color + x.direction}
            name={x.name}
            maxQuantity={x.maxQuantity}
            color={x.color}
            direction={x.direction}
          />
        ))}
      </Box>
      <Divider />
      <Flexbox gap={theme.mimirorg.spacing.base} justifyContent={"space-between"}>
        <Text variant={"body-medium"}>{t("terminal.summary.total")}</Text>
        <Text variant={"body-medium"}>{shownTerminalAmount}</Text>
      </Flexbox>
    </Box>
  );
};