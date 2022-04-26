import { css } from "styled-components/macro";

/**
 * TYPOGRAPHY SPECIFIC GLOBALS
 */
export const globalTypographyStyle = css`
  body {
    font-family: var(--tl-sys-font-family);
    font-weight: var(--tl-sys-font-weight-normal);
    font-size: var(--tl-sys-font-base-size);
    color: var(--tl-sys-color-on-background);
  }

  h1 {
    font: var(--tl-sys-font-h1);
  }

  h2 {
    font: var(--tl-sys-font-h2);
  }

  h3 {
    font: var(--tl-sys-font-h3);
  }

  h4 {
    font: var(--tl-sys-font-h4);
  }

  h5 {
    font: var(--tl-sys-font-h5);
  }

  p {
    font: var(--tl-sys-font-text);
  }

  b,
  strong {
    font-weight: var(--tl-sys-font-weight-bold);
  }

  a {
    font: var(--tl-sys-font-link);
  }

  a:hover {
    text-decoration: underline;
    cursor: pointer;
  }

  small {
    font: var(--tl-sys-font-subtext);
  }
`;
