import { css } from "styled-components/macro";
import { ColorTheme } from "../../core";

export const textButton = (color: ColorTheme) =>
  css`
    border: 0;
    background-color: transparent;
    color: ${color.primary.base};

    :disabled {
      color: ${color.surface.variant.on};
    }

    :not(:disabled) {
      :hover {
        background-color: ${color.secondary.base};
      }

      :active {
        background-color: ${color.tertiary.container?.base};
      }
    }
  `;
