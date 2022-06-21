import styled, { css } from "styled-components/macro";
import { ColorTheme, SpacingSystem } from "../../core";
import { TokenBaseProps } from "./Token";

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
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  padding: ${(props) => props.theme.tyle.spacing.base};

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

  ${({ interactive, ...props }) =>
    interactive &&
    css`
      :hover {
        cursor: pointer;
      }

      :active {
        background-color: ${props.theme.tyle.color.sys.tertiary.container?.base};
      }
    `};
`;

export const TokenContainer = styled.span<TokenBaseProps>`
  ${tokenBaseStyle}
`;

TokenContainer.defaultProps = {
  variant: "primary",
};

const primaryToken = (color: ColorTheme) =>
  css`
    background-color: ${color.tertiary.base};
    color: ${color.tertiary.on};
  `;

const secondaryToken = (color: ColorTheme, spacing: SpacingSystem) =>
  css`
    gap: ${spacing.base};
    background-color: ${color.background.base};
    color: ${color.tertiary.on};
    border: 1px solid ${color.tertiary.base};
  `;
