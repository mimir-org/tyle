import { useTheme } from "styled-components";
import { Box } from "../../layouts";
import { Text } from "../../text";
import { theme } from "../../core";

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
  const textVariant = `label-${variant}` as const;
  const contextTheme = useTheme();

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
      bgColor={contextTheme.color.surface.base}
      border={`1px solid ${contextTheme.color.outline.base}`}
    >
      <Text variant={textVariant} whiteSpace={"nowrap"} color={contextTheme.color.surface.on}>
        {text}
      </Text>
    </Box>
  );
};

const boxVariants = {
  small: {
    gap: theme.spacing.xxs,
    py: theme.spacing.xxs,
    px: theme.spacing.small,
  },
  medium: {
    gap: theme.spacing.xs,
    py: theme.spacing.xs,
    px: theme.spacing.medium,
  },
  large: {
    gap: theme.spacing.small,
    py: theme.spacing.small,
    px: theme.spacing.large,
  },
};
