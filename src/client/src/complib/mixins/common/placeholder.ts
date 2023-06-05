import { css } from "styled-components/macro";

/**
 * Common placeholder style for several components
 */
export const placeholder = css`
  ::placeholder {
    font: ${(props) => props.theme.tyle.typography.sys.roles.body.medium.font};
    letter-spacing: ${(props) => props.theme.tyle.typography.sys.roles.body.medium.letterSpacing};
    line-height: ${(props) => props.theme.tyle.typography.sys.roles.body.medium.lineHeight};
    color: ${(props) => props.theme.tyle.color.sys.outline.base};
    text-transform: none;
  }
`;
