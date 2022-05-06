import { css } from "styled-components/macro";
import { Positions } from "../props";

export const positionsMixin = css<Positions>`
  position: ${(props) => props.position};
  z-index: ${(props) => props.zIndex};
  top: ${(props) => props.top};
  right: ${(props) => props.right};
  bottom: ${(props) => props.bottom};
  left: ${(props) => props.left};
`;
