import { css } from "styled-components/macro";

/**
 * Common focus style for several components
 */
export const focus = css`
  :focus-visible {
    outline: 1px solid ${(props) => props.theme.tyle.color.sys.primary.base};
    outline-offset: 1px;
  }
`;
