import { css } from "styled-components/macro";
import { Display } from "../props";

export const displayMixin = css<Display>`
  display: ${(props) => props.display};
  overflow: ${(props) => props.overflow};
  text-overflow: ${(props) => props.textOverflow};
  visibility: ${(props) => props.visibility};
  white-space: ${(props) => props.whiteSpace};
`;
