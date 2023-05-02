import { Icon } from "complib/media";
import { TextTypes } from "complib/props";
import { Text } from "complib/text";
import { AspectObjectContainer, AspectObjectContainerProps } from "features/common/aspectobject/AspectObject.styled";
import { useTheme } from "styled-components";

export type AspectObjectProps = AspectObjectContainerProps & {
  name: string;
  img: string;
};

/**
 * Component which serves as the visual representation for an aspect object.
 *
 * @param name
 * @param color
 * @param img
 * @param variant
 * @constructor
 */
export const AspectObject = ({ name, img, color, variant = "small" }: AspectObjectProps) => {
  const theme = useTheme();
  const variantSpecs = AspectObjectVariantSpecs[variant];

  return (
    <AspectObjectContainer variant={variant} color={color}>
      <Text
        variant={variantSpecs.text.variant as TextTypes}
        color={theme.tyle.color.ref.neutral["0"]}
        textAlign={"center"}
      >
        {name}
      </Text>
      {img && <Icon size={variantSpecs.icon.size} src={img} alt="" />}
    </AspectObjectContainer>
  );
};

const AspectObjectVariantSpecs = {
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
