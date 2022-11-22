import { ColorTheme } from "complib/core";
import { css } from "styled-components/macro";

export const filledButton = (color: ColorTheme) =>
  css`
    border: 0;
    background-color: ${color.primary.base};
    color: ${color.primary.on};

    :disabled {
      background-color: ${color.outline.base};
      color: ${color.surface.variant.on};
    }

    :not(:disabled) {
      :hover {
        background-color: ${color.secondary.base};
        color: ${color.primary.base};
      }

      :active {
        background-color: ${color.surface.on};
        color: ${color.primary.on};
      }
    }
  `;
