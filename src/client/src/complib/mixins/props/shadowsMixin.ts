import { css } from "styled-components/macro";
import { Shadows } from "../../props";

export const shadowsMixin = css<Shadows>`
  box-shadow: ${(props) => props.boxShadow};
`;
