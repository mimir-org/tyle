import { TerminalItem } from "../../../../types/TerminalItem";
import { useTheme } from "styled-components";
import { Box } from "../../../../../../complib/layouts";
import { TerminalSingle } from "./TerminalSingle";
import { TerminalCollection } from "./TerminalCollection";

export interface TerminalsProps {
  terminals: TerminalItem[];
  variant?: "left" | "right";
  showCollectionLimit?: number;
}

/**
 * Component which houses a set of terminals.
 * Has the ability to switch between showing multiple terminals and a summary.
 *
 * @param terminals to display
 * @param variant decides which side of the component the summary should appear
 * @param showCollectionLimit threshold for switching to summary mode
 * @constructor
 */
export const Terminals = ({ terminals, variant, showCollectionLimit = 5 }: TerminalsProps) => {
  const theme = useTheme();
  const useSummary = terminals.length > showCollectionLimit;

  return (
    <Box display={"flex"} flexDirection={"column"} gap={theme.tyle.spacing.small}>
      {!useSummary &&
        terminals.map((terminal, index) => (
          <TerminalSingle {...terminal} key={terminal.name + terminal.direction + index} />
        ))}
      {useSummary && <TerminalCollection variant={variant} terminals={terminals} />}
    </Box>
  );
};
