import { Shadows } from "complib/props";
import { css } from "styled-components/macro";

export const shadowsMixin = css<Shadows>`
  box-shadow: ${(props) => props.boxShadow};
`;
