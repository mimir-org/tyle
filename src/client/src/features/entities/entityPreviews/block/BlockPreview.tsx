import { BlockTerminalItem } from "common/types/blockTerminalItem";
import { Box } from "@mimirorg/component-library";
import { Block } from "features/common/block/Block";
import { BlockVariant } from "features/common/block/Block.styled";
import { meetsInputCriteria, meetsOutputCriteria } from "features/entities/entityPreviews/block/BlockPreview.helpers";
import { TerminalButtonVariant, Terminals } from "features/common/terminal";

export interface BlockPreviewProps {
  name: string;
  color: string;
  img: string;
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
export const BlockPreview = ({ name, color, img, terminals, variant = "small" }: BlockPreviewProps) => {
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
      <Block name={name} color={color} img={img} variant={variantSpecs.block.variant as BlockVariant} />
      <Terminals
        terminals={outputSideTerminals}
        placement={"right"}
        variant={variantSpecs.terminals.variant as TerminalButtonVariant}
      />
    </Box>
  );
};

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
