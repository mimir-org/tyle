import { useTheme } from "styled-components";
import { Box } from "../../../../../../complib/layouts";
import { Icon } from "../../../../../../complib/media";
import { TextTypes } from "../../../../../../complib/props";
import { Text } from "../../../../../../complib/text";

export type NodeVariant = "small" | "large";

export interface NodeProps {
  name: string;
  color: string;
  img: string;
  variant?: NodeVariant;
}

/**
 * Component which serves as the visual representation for a node.
 *
 * @param name
 * @param color
 * @param img
 * @param variant
 * @constructor
 */
export const Node = ({ name, color, img, variant = "small" }: NodeProps) => {
  const theme = useTheme();
  const variantSpecs = NodeVariantSpecs[variant];

  return (
    <Box
      display={"flex"}
      flexDirection={"column"}
      justifyContent={"start"}
      alignItems={"center"}
      gap={variantSpecs.gap}
      height={variantSpecs.height}
      width={variantSpecs.width}
      p={theme.tyle.spacing.xl}
      bgColor={color}
      borderRadius={theme.tyle.border.radius.large}
    >
      <Text variant={variantSpecs.text.variant as TextTypes} color={theme.tyle.color.ref.neutral["0"]}>
        {name}
      </Text>
      {img && <Icon size={variantSpecs.icon.size} src={img} alt="" />}
    </Box>
  );
};

const NodeVariantSpecs = {
  small: {
    gap: "12px",
    width: "150px",
    height: "100px",
    text: {
      variant: "label-small",
    },
    icon: {
      size: 24,
    },
  },
  large: {
    gap: "24px",
    width: "250px",
    height: "150px",
    text: {
      variant: "label-large",
    },
    icon: {
      size: 30,
    },
  },
};
