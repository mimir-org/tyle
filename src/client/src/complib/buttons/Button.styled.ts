import styled, { css } from "styled-components/macro";
import { Polymorphic, Positions } from "../props";
import { ButtonHTMLAttributes, ElementType } from "react";

export type ButtonContainerProps = Omit<Positions, "zIndex"> &
  Polymorphic<ElementType> &
  ButtonHTMLAttributes<HTMLButtonElement> & {
    variant?: "filled" | "outlined" | "text";
  };

export const ButtonContainer = styled.button<ButtonContainerProps>`
  position: relative;
  display: inline-flex;
  justify-content: center;
  align-items: center;
  gap: ${(props) => props.theme.typeLibrary.spacing.xs};
  white-space: nowrap;
  text-decoration: none;

  font: ${(props) => props.theme.typeLibrary.typography.label.large.font};
  line-height: ${(props) => props.theme.typeLibrary.typography.label.large.lineHeight};
  letter-spacing: ${(props) => props.theme.typeLibrary.typography.label.large.letterSpacing};

  padding: ${(props) => props.theme.typeLibrary.spacing.xs} ${(props) => props.theme.typeLibrary.spacing.small};
  border-radius: 999px;

  ${(props) =>
    props.variant === "filled" &&
    css`
      border: 0;
      background-color: ${props.theme.typeLibrary.color.primary.base};
      color: ${props.theme.typeLibrary.color.primary.on};
    `};

  ${(props) =>
    props.variant === "outlined" &&
    css`
      background-color: transparent;
      border: 1px solid ${props.theme.typeLibrary.color.outline.base};
      color: ${props.theme.typeLibrary.color.primary.base};
    `};

  ${(props) =>
    props.variant === "text" &&
    css`
      border: 0;
      background-color: transparent;
      color: ${props.theme.typeLibrary.color.primary.base};
    `};

  :hover {
    cursor: pointer;
  }

  :disabled {
    cursor: not-allowed;
  }
`;

ButtonContainer.defaultProps = {
  variant: "filled",
};
