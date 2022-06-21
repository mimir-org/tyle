import { useTheme } from "styled-components";
import { Box } from "../../../../../../complib/layouts";
import { TerminalItem } from "../../../../types/TerminalItem";
import { TerminalButtonProps } from "./TerminalButton";
import { TerminalCollection } from "./TerminalCollection";
import { TerminalSingle } from "./TerminalSingle";

export type TerminalsProps = Pick<TerminalButtonProps, "variant"> & {
  terminals: TerminalItem[];
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
export const Terminals = ({ terminals, placement, variant, showCollectionLimit = 5 }: TerminalsProps) => {
  const theme = useTheme();
  const useSummary = terminals.length > showCollectionLimit;

  return (
    <Box display={"flex"} flexDirection={"column"} gap={theme.tyle.spacing.xs}>
      {!useSummary &&
        terminals.map((terminal, index) => (
          <TerminalSingle variant={variant} {...terminal} key={terminal.name + terminal.direction + index} />
        ))}
      {useSummary && <TerminalCollection placement={placement} terminals={terminals} />}
    </Box>
  );
};
