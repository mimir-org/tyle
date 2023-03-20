import { ColorTheme } from "complib/core";
import { css } from "styled-components/macro";

export const outlinedButton = (color: ColorTheme, dangerousAction?: boolean) =>
  css`
    outline: 0;
    background-color: transparent;
    border: 1px solid ${color.primary.base};
    color: ${dangerousAction ? color.dangerousAction.base: color.primary.base};

    :disabled {
      color: ${color.surface.variant.on};
      border-color: ${color.outline.base};
    }

    :not(:disabled) {
      :hover {
        background-color: ${dangerousAction ? color.dangerousAction.on : color.secondary.base};
      }

      :active {
        background-color: ${color.tertiary.container?.base};
      }
    }
  `;
