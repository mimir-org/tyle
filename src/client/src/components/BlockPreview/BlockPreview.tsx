import { Box } from "@mimirorg/component-library";
import Block from "components/Block";
import { BlockVariant } from "components/Block/Block.styled";
import { TerminalButtonVariant } from "components/TerminalButton";
import { BlockTerminalItem } from "types/blockTerminalItem";
import { EngineeringSymbol } from "types/blocks/engineeringSymbol";
import { meetsInputCriteria, meetsOutputCriteria } from "./BlockPreview.helpers";
import Terminals from "./Terminals";

export interface BlockPreviewProps {
  name: string;
  color: string;
  symbol: EngineeringSymbol | null;
  terminals: BlockTerminalItem[];
  variant?: "small" | "large";
}

/**
 * Components which presents a visual representation of a block,
 * this includes the block itself and its inputs, outputs etc.
 *
 * @param name
 * @param color
 * @param img
 * @param terminals
 * @param variant
 * @constructor
 */
const BlockPreview = ({ name, color, symbol, terminals, variant = "small" }: BlockPreviewProps) => {
  const inputSideTerminals = terminals.filter((t) => meetsInputCriteria(t.direction));
  const outputSideTerminals = terminals.filter((t) => meetsOutputCriteria(t.direction));
  const variantSpecs = BlockPreviewVariantSpec[variant];

  return (
    <Box display={"flex"} alignSelf={"center"} alignItems={"center"}>
      <Terminals
        terminals={inputSideTerminals}
        placement={"left"}
        variant={variantSpecs.terminals.variant as TerminalButtonVariant}
      />
      <Block name={name} color={color} symbol={symbol} variant={variantSpecs.block.variant as BlockVariant} />
      <Terminals
        terminals={outputSideTerminals}
        placement={"right"}
        variant={variantSpecs.terminals.variant as TerminalButtonVariant}
      />
    </Box>
  );
};

export default BlockPreview;

const BlockPreviewVariantSpec = {
  small: {
    block: {
      variant: "small",
    },
    terminals: {
      variant: "small",
    },
  },
  large: {
    block: {
      variant: "large",
    },
    terminals: {
      variant: "medium",
    },
  },
};
