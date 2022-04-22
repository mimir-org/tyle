import { Box } from "../../layouts";
import { Text } from "../../text";
import { THEME } from "../../core";

interface TokenProps {
  text: string;
  variant?: "small" | "medium" | "large";
}

/**
 * A component for representing a piece of data.
 * Often used to display a collection of related attributes.
 *
 * @param text to be displayed inside token
 * @param variant controls size of token
 * @constructor
 */
export const Token = ({ text, variant = "medium" }: TokenProps) => {
  const boxProps = boxVariants[variant];
  const textProps = textVariants[variant];

  return (
    <Box
      as={"span"}
      {...boxProps}
      display={"flex"}
      alignItems={"center"}
      justifyContent={"center"}
      borderRadius={"999px"}
      width={"fit-content"}
      maxWidth={"100%"}
      bgColor={THEME.COLOR.SECONDARY.BASE}
    >
      <Text {...textProps} whiteSpace={"nowrap"} color={THEME.COLOR.TEXT.PRIMARY.INVERTED}>
        {text}
      </Text>
    </Box>
  );
};

const boxVariants = {
  small: {
    gap: THEME.SPACING.XXS,
    py: THEME.SPACING.XXS,
    px: THEME.SPACING.SMALL,
  },
  medium: {
    gap: THEME.SPACING.XS,
    py: THEME.SPACING.XS,
    px: THEME.SPACING.MEDIUM,
  },
  large: {
    gap: THEME.SPACING.SMALL,
    py: THEME.SPACING.SMALL,
    px: THEME.SPACING.LARGE,
  },
};

const textVariants = {
  small: {
    fontSize: THEME.FONT.SIZES.SMALL,
  },
  medium: {
    fontSize: THEME.FONT.SIZES.BASE,
  },
  large: {
    fontSize: THEME.FONT.SIZES.MEDIUM,
  },
};
