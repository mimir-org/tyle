import { Box } from "@mimirorg/component-library";
import { TerminalButtonProps } from "components/TerminalButton";
import { useTheme } from "styled-components";
import { BlockTerminalItem } from "types/blockTerminalItem";
import TerminalCollection from "./TerminalCollection";
import TerminalSingle from "./TerminalSingle";

export type TerminalsProps = Pick<TerminalButtonProps, "variant"> & {
  terminals: BlockTerminalItem[];
  placement?: "left" | "right";
  showCollectionLimit?: number;
};

/**
 * Component which houses a set of terminals.
 * Has the ability to switch between showing multiple terminals and a summary.
 *
 * @param terminals to display
 * @param placement decides which side of the component the summary should appear
 * @param variant decides the dimensions of the terminals themselves
 * @param showCollectionLimit threshold for switching to summary mode
 * @constructor
 */
const Terminals = ({ terminals, placement, variant, showCollectionLimit = 5 }: TerminalsProps) => {
  const theme = useTheme();
  const useSummary = terminals.length > showCollectionLimit;
  const alignment = placement === "right" ? "start" : "end";

  return (
    <Box
      display={"flex"}
      flexDirection={"column"}
      gap={theme.mimirorg.spacing.xs}
      minWidth={"30px"}
      alignItems={alignment}
    >
      {!useSummary &&
        terminals.map((terminal, index) => (
          <TerminalSingle variant={variant} {...terminal} key={terminal.id + terminal.direction + index} />
        ))}
      {useSummary && <TerminalCollection placement={placement} terminals={terminals} />}
    </Box>
  );
};

export default Terminals;
