import { css } from "styled-components/macro";
import { ColorTheme, SpacingSystem } from "../../../core";

export const secondaryToken = (color: ColorTheme, spacing: SpacingSystem) =>
  css`
    gap: ${spacing.base};
    background-color: ${color.background.base};
    color: ${color.tertiary.on};
    border: 1px solid ${color.tertiary.base};
    border-radius: ${(props) => props.theme.tyle.border.radius.medium};
  `;
