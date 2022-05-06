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
    gap: theme.typeLibrary.spacing.xxs,
    p: `${theme.typeLibrary.spacing.xs} ${theme.typeLibrary.spacing.small}`,
  },
  medium: {
    gap: theme.typeLibrary.spacing.xs,
    p: `${theme.typeLibrary.spacing.xs} ${theme.typeLibrary.spacing.medium}`,
  },
  large: {
    gap: theme.typeLibrary.spacing.small,
    p: `${theme.typeLibrary.spacing.small} ${theme.typeLibrary.spacing.large}`,
  },
});
