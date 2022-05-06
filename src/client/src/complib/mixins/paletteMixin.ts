import { css } from "styled-components/macro";
import { Palette } from "../props";

export const paletteMixin = css<Palette>`
  color: ${(props) => props.color};
  background-color: ${(props) => props.bgColor};
`;
