import { TerminalItem } from "../../../../types/TerminalItem";
import { Divider, Popover } from "../../../../../../complib/data-display";
import { VisuallyHidden } from "../../../../../../complib/accessibility";
import { TerminalButton } from "./TerminalButton";
import { useTheme } from "styled-components";
import { Box } from "../../../../../../complib/layouts";
import { TerminalDescription } from "./TerminalSingle";
import { Text } from "../../../../../../complib/text";

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
        <VisuallyHidden>Open terminal summary</VisuallyHidden>
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
    <Box display={"flex"} gap={theme.tyle.spacing.small} flexDirection={"column"} maxWidth={"250px"}>
      <Box
        display={"flex"}
        gap={theme.tyle.spacing.small}
        flexDirection={"column"}
        maxHeight={"250px"}
        overflow={"auto"}
      >
        {terminals.map((t) => (
          <TerminalDescription key={t.name} name={t.name} amount={t.amount} color={t.color} direction={t.direction} />
        ))}
      </Box>
      <Divider />
      <Text>Total amount of terminals: {totalTerminalAmount}</Text>
    </Box>
  );
};
