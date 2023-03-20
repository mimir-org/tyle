import { ColorTheme } from "complib/core";
import { css } from "styled-components/macro";

export const filledButton = (color: ColorTheme, dangerousAction?: boolean) =>
  css`
    border: 0;
    background-color: ${dangerousAction ? color.dangerousAction.base : color.primary.base};
    color: ${dangerousAction ? color.dangerousAction.on : color.primary.on};

    :disabled {
      background-color: ${color.outline.base};
      color: ${color.surface.variant.on};
    }

    :not(:disabled) {
      :hover {
        background-color: ${dangerousAction ? color.dangerousAction.on : color.secondary.base};
        color: ${dangerousAction ? color.dangerousAction.base : color.primary.base};
      }

      :active {
        background-color: ${color.surface.on};
        color: ${color.primary.on};
      }
    }
  `;
