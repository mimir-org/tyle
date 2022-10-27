import { Icon } from "complib/media";
import { TextTypes } from "complib/props";
import { Text } from "complib/text";
import { useTheme } from "styled-components";
import { NodeContainer, NodeContainerProps } from "./Node.styled";

export type NodeProps = NodeContainerProps & {
  name: string;
  img: string;
};

/**
 * Component which serves as the visual representation for a node.
 *
 * @param name
 * @param color
 * @param img
 * @param variant
 * @constructor
 */
export const Node = ({ name, img, color, variant = "small" }: NodeProps) => {
  const theme = useTheme();
  const variantSpecs = NodeVariantSpecs[variant];

  return (
    <NodeContainer variant={variant} color={color}>
      <Text
        variant={variantSpecs.text.variant as TextTypes}
        color={theme.tyle.color.ref.neutral["0"]}
        textAlign={"center"}
      >
        {name}
      </Text>
      {img && <Icon size={variantSpecs.icon.size} src={img} alt="" />}
    </NodeContainer>
  );
};

const NodeVariantSpecs = {
  small: {
    text: {
      variant: "label-small",
    },
    icon: {
      size: 24,
    },
  },
  large: {
    text: {
      variant: "label-large",
    },
    icon: {
      size: 30,
    },
  },
};
