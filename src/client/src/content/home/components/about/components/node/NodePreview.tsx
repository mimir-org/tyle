import { Box } from "../../../../../../complib/layouts";
import { Node } from "./Node";
import { Terminals } from "../terminal/Terminals";
import { TerminalItem } from "../../../../types/TerminalItem";
import { useTheme } from "styled-components/macro";
import { meetsInputCriteria, meetsOutputCriteria } from "./NodePreview.helpers";

export interface NodePreviewProps {
  color: string;
  img: string;
  terminals: TerminalItem[];
}

/**
 * Components which presents a visual representation of a node,
 * this includes the node itself and its inputs, outputs etc.
 *
 * @param color
 * @param img
 * @param terminals
 * @constructor
 */
export const NodePreview = ({ color, img, terminals }: NodePreviewProps) => {
  const theme = useTheme();
  const inputSideTerminals = terminals.filter((t) => meetsInputCriteria(t.direction));
  const outputSideTerminals = terminals.filter((t) => meetsOutputCriteria(t.direction));

  return (
    <Box display={"flex"} alignSelf={"center"} alignItems={"center"} gap={theme.tyle.spacing.xs}>
      <Terminals terminals={inputSideTerminals} variant={"left"} />
      <Node color={color} img={img} width={"320px"} height={"180px"} imgSize={40} />
      <Terminals terminals={outputSideTerminals} variant={"right"} />
    </Box>
  );
};
