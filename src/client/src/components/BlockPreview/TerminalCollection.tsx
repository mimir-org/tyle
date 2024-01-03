import { Divider, Popover } from "@mimirorg/component-library";
import Box from "components/Box";
import Flexbox from "components/Flexbox";
import TerminalButton from "components/TerminalButton";
import Text from "components/Text";
import VisuallyHidden from "components/VisuallyHidden";
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

  return (
    <Popover placement={placement} content={<TerminalCollectionDescription terminals={terminals} />}>
      <TerminalButton variant={"large"} color={theme.tyle.color.reference.primary["40"]}>
        <VisuallyHidden>Open terminal summary</VisuallyHidden>
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
  const totalTerminalAmount = terminals.reduce((sum, terminal) => sum + (terminal.maxQuantity ?? 0), 0);
  const shownTerminalAmount = totalTerminalAmount >= MAXIMUM_TERMINAL_QUANTITY_VALUE ? "Infinite" : totalTerminalAmount;

  return (
    <Box display={"flex"} gap={theme.tyle.spacing.l} flexDirection={"column"} maxWidth={"250px"}>
      <Text variant={"title-small"}>Summary</Text>
      <Box display={"flex"} gap={theme.tyle.spacing.l} flexDirection={"column"} maxHeight={"250px"} overflow={"auto"}>
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
      <Flexbox gap={theme.tyle.spacing.base} justifyContent={"space-between"}>
        <Text variant={"body-medium"}>Total terminals:</Text>
        <Text variant={"body-medium"}>{shownTerminalAmount}</Text>
      </Flexbox>
    </Box>
  );
};
