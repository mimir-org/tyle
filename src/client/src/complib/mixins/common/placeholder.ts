import { css } from "styled-components/macro";

/**
 * Common placeholder style for several components
 */
export const placeholder = css`
  ::placeholder {
    color: ${(props) => props.theme.tyle.color.sys.outline.base};
  }
`;
