import { ColorTheme } from "complib/core";
import { css } from "styled-components/macro";

export const textButton = (color: ColorTheme, dangerousAction?: boolean) =>
  css`
    border: 0;
    background-color: transparent;
    color: ${dangerousAction ? color.dangerousAction.on : color.primary.base};

    :disabled {
      color: ${color.surface.variant.on};
    }

    :not(:disabled) {
      :hover {
        background-color: ${dangerousAction? color.dangerousAction.on: color.secondary.base};
      }

      :active {
        background-color: ${color.tertiary.container?.base};
      }
    }
  `;
