import { Palette } from "complib/props";
import { css } from "styled-components/macro";

export const paletteMixin = css<Palette>`
  color: ${(props) => props.color};
  background-color: ${(props) => props.bgColor};
`;
