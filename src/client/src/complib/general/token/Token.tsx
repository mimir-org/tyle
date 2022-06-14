import { Text } from "../../text";
import { TokenContainer } from "./Token.styled";
import { DefaultTheme, useTheme } from "styled-components";

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
  const boxProps = boxVariants(useTheme())[variant];
  const textVariant = `label-${variant}` as const;

  return (
    <TokenContainer as={"span"} {...boxProps}>
      <Text variant={textVariant} whiteSpace={"nowrap"}>
        {text}
      </Text>
    </TokenContainer>
  );
};

const boxVariants = (theme: DefaultTheme) => ({
  small: {
    gap: theme.tyle.spacing.xxs,
    p: `${theme.tyle.spacing.xs} ${theme.tyle.spacing.small}`,
  },
  medium: {
    gap: theme.tyle.spacing.xs,
    p: `${theme.tyle.spacing.xs} ${theme.tyle.spacing.medium}`,
  },
  large: {
    gap: theme.tyle.spacing.small,
    p: `${theme.tyle.spacing.small} ${theme.tyle.spacing.large}`,
  },
});
