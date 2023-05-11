import { AspectObjectTerminalItem } from "common/types/aspectObjectTerminalItem";
import { Box } from "complib/layouts";
import { AspectObject } from "features/common/aspectobject/AspectObject";
import { AspectObjectVariant } from "features/common/aspectobject/AspectObject.styled";
import {
  meetsInputCriteria,
  meetsOutputCriteria,
} from "features/entities/entityPreviews/aspectobject/AspectObjectPreview.helpers";
import { TerminalButtonVariant, Terminals } from "features/common/terminal";

export interface AspectObjectPreviewProps {
  name: string;
  color: string;
  img: string;
  terminals: AspectObjectTerminalItem[];
  variant?: "small" | "large";
}

/**
 * Components which presents a visual representation of an aspect object,
 * this includes the aspect object itself and its inputs, outputs etc.
 *
 * @param name
 * @param color
 * @param img
 * @param terminals
 * @param variant
 * @constructor
 */
export const AspectObjectPreview = ({ name, color, img, terminals, variant = "small" }: AspectObjectPreviewProps) => {
  const inputSideTerminals = terminals.filter((t) => meetsInputCriteria(t.direction));
  const outputSideTerminals = terminals.filter((t) => meetsOutputCriteria(t.direction));
  const variantSpecs = AspectObjectPreviewVariantSpec[variant];

  return (
    <Box display={"flex"} alignSelf={"center"} alignItems={"center"}>
      <Terminals
        terminals={inputSideTerminals}
        placement={"left"}
        variant={variantSpecs.terminals.variant as TerminalButtonVariant}
      />
      <AspectObject
        name={name}
        color={color}
        img={img}
        variant={variantSpecs.aspectObject.variant as AspectObjectVariant}
      />
      <Terminals
        terminals={outputSideTerminals}
        placement={"right"}
        variant={variantSpecs.terminals.variant as TerminalButtonVariant}
      />
    </Box>
  );
};

const AspectObjectPreviewVariantSpec = {
  small: {
    aspectObject: {
      variant: "small",
    },
    terminals: {
      variant: "small",
    },
  },
  large: {
    aspectObject: {
      variant: "large",
    },
    terminals: {
      variant: "medium",
    },
  },
};
