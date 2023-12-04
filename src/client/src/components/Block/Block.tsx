import { Text } from "@mimirorg/component-library";
import EngineeringSymbolSvg from "components/EngineeringSymbolSvg";
import { useTheme } from "styled-components";
import { EngineeringSymbol } from "types/blocks/engineeringSymbol";
import BlockContainer, { BlockContainerProps } from "./Block.styled";

export type BlockProps = BlockContainerProps & {
  name: string;
  symbol: EngineeringSymbol | null;
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
const Block = ({ name, symbol, color, variant = "small" }: BlockProps) => {
  const theme = useTheme();
  const variantSpecs = BlockVariantSpecs[variant];

  return (
    <BlockContainer variant={variant} color={color}>
      {symbol && (
        <EngineeringSymbolSvg symbol={symbol} width={variantSpecs.icon.size} height={variantSpecs.icon.size} />
      )}
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
