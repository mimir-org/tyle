import { NodeTerminalItem } from "common/types/nodeTerminalItem";
import { Box } from "complib/layouts";
import { Node } from "features/common/node/Node";
import { NodeVariant } from "features/common/node/Node.styled";
import { meetsInputCriteria, meetsOutputCriteria } from "features/common/node/NodePreview.helpers";
import { TerminalButtonVariant, Terminals } from "features/common/terminal";

export interface NodePreviewProps {
  name: string;
  color: string;
  img: string;
  terminals: NodeTerminalItem[];
  variant?: "small" | "large";
}

/**
 * Components which presents a visual representation of a node,
 * this includes the node itself and its inputs, outputs etc.
 *
 * @param name
 * @param color
 * @param img
 * @param terminals
 * @param variant
 * @constructor
 */
export const NodePreview = ({ name, color, img, terminals, variant = "small" }: NodePreviewProps) => {
  const inputSideTerminals = terminals.filter((t) => meetsInputCriteria(t.direction));
  const outputSideTerminals = terminals.filter((t) => meetsOutputCriteria(t.direction));
  const variantSpecs = NodePreviewVariantSpec[variant];

  return (
    <Box display={"flex"} alignSelf={"center"} alignItems={"center"}>
      <Terminals
        terminals={inputSideTerminals}
        placement={"left"}
        variant={variantSpecs.terminals.variant as TerminalButtonVariant}
      />
      <Node name={name} color={color} img={img} variant={variantSpecs.node.variant as NodeVariant} />
      <Terminals
        terminals={outputSideTerminals}
        placement={"right"}
        variant={variantSpecs.terminals.variant as TerminalButtonVariant}
      />
    </Box>
  );
};

const NodePreviewVariantSpec = {
  small: {
    node: {
      variant: "small",
    },
    terminals: {
      variant: "small",
    },
  },
  large: {
    node: {
      variant: "large",
    },
    terminals: {
      variant: "medium",
    },
  },
};
