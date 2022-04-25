import { css } from "styled-components/macro";
import { Sizing } from "../props";

export const sizingMixin = css<Sizing>`
  width: ${(props) => props.width};
  max-width: ${(props) => props.maxWidth};
  min-width: ${(props) => props.minWidth};
  height: ${(props) => props.height};
  max-height: ${(props) => props.maxHeight};
  min-height: ${(props) => props.minHeight};
  box-sizing: ${(props) => props.boxSizing};
`;
