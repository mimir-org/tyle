import { TokenBaseProps } from "complib/general/token/Token";
import { primaryToken } from "complib/general/token/variants/primaryToken";
import { secondaryToken } from "complib/general/token/variants/secondaryToken";
import { focus } from "complib/mixins";
import { motion } from "framer-motion";
import styled, { css } from "styled-components/macro";

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
    const {
      color: { sys },
      spacing,
    } = props.theme.tyle;

    switch (variant) {
      case "primary": {
        return primaryToken(sys);
      }
      case "secondary": {
        return secondaryToken(sys, spacing);
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
        background-color: ${props.theme.tyle.color.sys.tertiary.container?.base};
      }
    `};

  ${({ $selected, ...props }) =>
    $selected &&
    css`
      background-color: ${props.theme.tyle.color.sys.tertiary.container?.base};
    `};
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
