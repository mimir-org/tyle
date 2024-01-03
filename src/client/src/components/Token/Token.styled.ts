import { RadioGroup } from "@radix-ui/react-radio-group";
import { ColorSystem } from "components/TyleThemeProvider/color";
import { SpacingSystem } from "components/TyleThemeProvider/spacing";
import { motion } from "framer-motion";
import { focus } from "styleConstants";
import styled, { css } from "styled-components";
import { TokenBaseProps } from "./Token";

const primaryToken = (color: ColorSystem) => css`
  background-color: ${color.tertiary.base};
  color: ${color.tertiary.on};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
`;

const secondaryToken = (color: ColorSystem, spacing: SpacingSystem) => css`
  gap: ${spacing.base};
  background-color: ${color.background.base};
  color: ${color.tertiary.on};
  border: 1px solid ${color.tertiary.base};
  border-radius: ${(props) => props.theme.tyle.border.radius.medium};
`;

export const tokenBaseStyle = css<TokenBaseProps>`
  display: flex;
  align-items: center;
  justify-content: center;
  gap: ${(props) => props.theme.tyle.spacing.s};

  height: 32px;
  max-width: 166px;
  min-width: 65px;
  width: fit-content;

  border: 0;
  padding: ${(props) => props.theme.tyle.spacing.base};

  ${focus};

  ${({ variant, ...props }) => {
    const { color, spacing } = props.theme.tyle;

    switch (variant) {
      case "primary": {
        return primaryToken(color);
      }
      case "secondary": {
        return secondaryToken(color, spacing);
      }
    }
  }};

  ${({ $interactive, ...props }) =>
    $interactive &&
    css`
      :hover {
        cursor: pointer;
      }

      :active {
        background-color: ${props.theme.tyle.color.tertiary.container?.base};
      }
    `};

  ${({ $selected, ...props }) =>
    $selected &&
    css`
      background-color: ${props.theme.tyle.color.tertiary.container?.base};
    `};

  &[aria-checked="true"] {
    background-color: ${(props) => props.theme.tyle.color.tertiary.container?.base};
  }
`;

export const TokenContainer = styled.span<TokenBaseProps>`
  ${tokenBaseStyle}
`;

TokenContainer.defaultProps = {
  variant: "primary",
};

/**
 * An animation wrapper for the TokenContainer component
 *
 * @see https://github.com/framer/motion
 */
export const MotionTokenContainer = motion(TokenContainer);

export const TokenRadioGroupRoot = styled(RadioGroup)`
  display: flex;
  flex-wrap: wrap;
  gap: ${(props) => props.theme.tyle.spacing.base};
`;
