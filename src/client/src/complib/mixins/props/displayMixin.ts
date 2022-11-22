import { Display } from "complib/props";
import { css } from "styled-components/macro";

export const displayMixin = css<Display>`
  display: ${(props) => props.display};
  overflow: ${(props) => props.overflow};
  text-overflow: ${(props) => props.textOverflow};
  visibility: ${(props) => props.visibility};
  white-space: ${(props) => props.whiteSpace};
`;
