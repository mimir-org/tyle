import { useTheme } from "styled-components";
import { TextResources } from "../../../../../../assets/text";
import { VisuallyHidden } from "../../../../../../complib/accessibility";
import { Divider, Popover } from "../../../../../../complib/data-display";
import { Box, Flexbox } from "../../../../../../complib/layouts";
import { Text } from "../../../../../../complib/text";
import { TerminalItem } from "../../../../types/TerminalItem";
import { TerminalButton } from "./TerminalButton";
import { TerminalDescription } from "./TerminalSingle";

interface TerminalCollectionProps {
  terminals: TerminalItem[];
  variant?: "left" | "right";
}

/**
 * Displays multiple terminals in a popover which is shown when clicking on this component.
 *
 * @param terminals to show inside popover
 * @param variant decides which side the popover should appear
 * @constructor
 */
export const TerminalCollection = ({ terminals, variant }: TerminalCollectionProps) => {
  return (
    <Popover placement={variant} content={<TerminalCollectionDescription terminals={terminals} />}>
      <TerminalButton size={30} color={"#6e6e6e"}>
        <VisuallyHidden>{TextResources.TERMINAL_OPEN_SUMMARY}</VisuallyHidden>
      </TerminalButton>
    </Popover>
  );
};

interface TerminalCollectionDescriptionProps {
  terminals: TerminalItem[];
}

const TerminalCollectionDescription = ({ terminals }: TerminalCollectionDescriptionProps) => {
  const theme = useTheme();
  const totalTerminalAmount = terminals.reduce((sum, terminal) => sum + terminal.amount, 0);

  return (
    <Box display={"flex"} gap={theme.tyle.spacing.l} flexDirection={"column"} maxWidth={"250px"}>
      <Text variant={"title-medium"}>{TextResources.TERMINAL_SUMMARY}</Text>
      <Box display={"flex"} gap={theme.tyle.spacing.l} flexDirection={"column"} maxHeight={"250px"} overflow={"auto"}>
        {terminals.map((t) => (
          <TerminalDescription key={t.name} name={t.name} amount={t.amount} color={t.color} direction={t.direction} />
        ))}
      </Box>
      <Divider />
      <Flexbox gap={theme.tyle.spacing.base} justifyContent={"space-between"}>
        <Text>{TextResources.TERMINAL_TOTAL}:</Text>
        <Text>{totalTerminalAmount}</Text>
      </Flexbox>
    </Box>
  );
};
