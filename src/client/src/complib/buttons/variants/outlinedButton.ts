import { ColorTheme } from "complib/core";
import { css } from "styled-components/macro";

export const outlinedButton = (color: ColorTheme, dangerousAction?: boolean) =>
  css`
    outline: 0;
    background-color: transparent;
    border: 1px solid ${dangerousAction ? color.dangerousAction.on : color.primary.base};
    color: ${dangerousAction ? color.dangerousAction.on: color.primary.base};

    :disabled {
      color: ${color.surface.variant.on};
      border-color: ${color.outline.base};
    }

    :not(:disabled) {
      :hover {
        background-color: ${dangerousAction ? color.dangerousAction.on: color.secondary.base};
        color: ${dangerousAction ? color.dangerousAction.base: color.primary.base};
      }

      :active {
        background-color: ${dangerousAction ? color.dangerousAction.base : color.tertiary.container?.base};
      }
    }
  `;
