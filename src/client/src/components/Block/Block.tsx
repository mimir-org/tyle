import { Icon, Text } from "@mimirorg/component-library";
import { BlockContainer, BlockContainerProps } from "components/Block/Block.styled";
import { useTheme } from "styled-components";

export type BlockProps = BlockContainerProps & {
  name: string;
  img: string;
};

/**
 * Component which serves as the visual representation for a block.
 *
 * @param name
 * @param color
 * @param img
 * @param variant
 * @constructor
 */
const Block = ({ name, img, color, variant = "small" }: BlockProps) => {
  const theme = useTheme();
  const variantSpecs = BlockVariantSpecs[variant];

  return (
    <BlockContainer variant={variant} color={color}>
      {img && <Icon size={variantSpecs.icon.size} src={img} alt="" />}
      <Text variant={"title-medium"} color={theme.mimirorg.color.reference.neutral["0"]} textAlign={"center"}>
        {name}
      </Text>
    </BlockContainer>
  );
};

export default Block;

const BlockVariantSpecs = {
  small: {
    text: {
      variant: "label-small",
    },
    icon: {
      size: 35,
    },
  },
  large: {
    text: {
      variant: "label-large",
    },
    icon: {
      size: 40,
    },
  },
};
