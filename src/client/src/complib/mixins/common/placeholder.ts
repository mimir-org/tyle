import { css } from "styled-components/macro";

/**
 * Common placeholder style for several components
 */
export const placeholder = css`
  ::placeholder {
    font: ${(props) => props.theme.mimirorg.typography.roles.body.medium.font};
    letter-spacing: ${(props) => props.theme.mimirorg.typography.roles.body.medium.letterSpacing};
    line-height: ${(props) => props.theme.mimirorg.typography.roles.body.medium.lineHeight};
    color: ${(props) => props.theme.mimirorg.color.outline.base};
    text-transform: none;
  }
`;
