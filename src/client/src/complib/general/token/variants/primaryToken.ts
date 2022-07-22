import { css } from "styled-components/macro";
import { ColorTheme } from "../../../core";

export const primaryToken = (color: ColorTheme) =>
  css`
    background-color: ${color.tertiary.base};
    color: ${color.tertiary.on};
    border-radius: ${(props) => props.theme.tyle.border.radius.large};
  `;
