import styled, { css } from "styled-components/macro";
import { Polymorphic, Positions } from "../props";
import { ButtonHTMLAttributes, ElementType } from "react";
import { layeredColor, translucify } from "../mixins";

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

      :disabled {
        background-color: ${translucify(props.theme.typeLibrary.color.surface.on, 0.12)};
        color: ${translucify(props.theme.typeLibrary.color.surface.on, 0.38)};
      }

      :not(:disabled) {
        :hover {
          background: ${layeredColor(
            {
              color: props.theme.typeLibrary.color.primary.base,
              opacity: 0.05,
            },
            {
              color: props.theme.typeLibrary.color.primary.on,
              opacity: 0.08,
            },
            { color: props.theme.typeLibrary.color.primary.base, opacity: 1 }
          )};
        }

        :active {
          background: ${layeredColor(
            {
              color: props.theme.typeLibrary.color.primary.on,
              opacity: 0.12,
            },
            { color: props.theme.typeLibrary.color.primary.base, opacity: 1 }
          )};
        }
      }
    `};

  ${(props) =>
    props.variant === "outlined" &&
    css`
      background-color: transparent;
      border: 1px solid ${props.theme.typeLibrary.color.outline.base};
      color: ${props.theme.typeLibrary.color.primary.base};

      :disabled {
        border-color: ${translucify(props.theme.typeLibrary.color.surface.on, 0.12)};
        color: ${translucify(props.theme.typeLibrary.color.surface.on, 0.38)};
      }

      :not(:disabled) {
        :hover {
          background-color: ${translucify(props.theme.typeLibrary.color.primary.base, 0.08)};
        }

        :active {
          background-color: ${translucify(props.theme.typeLibrary.color.primary.base, 0.12)};
        }
      }
    `};

  ${(props) =>
    props.variant === "text" &&
    css`
      border: 0;
      background-color: transparent;
      color: ${props.theme.typeLibrary.color.primary.base};

      :disabled {
        color: ${translucify(props.theme.typeLibrary.color.surface.on, 0.38)};
      }

      :not(:disabled) {
        :hover {
          background-color: ${translucify(props.theme.typeLibrary.color.primary.base, 0.08)};
        }

        :active {
          background-color: ${translucify(props.theme.typeLibrary.color.primary.base, 0.12)};
        }
      }
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
