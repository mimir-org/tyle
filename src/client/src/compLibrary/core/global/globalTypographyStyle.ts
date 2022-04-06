import { css } from "styled-components/macro";

/**
 * TYPOGRAPHY SPECIFIC GLOBALS
 */
export const globalTypographyStyle = css`
  body {
    font-family: var(--font-family);
    font-weight: var(--font-weight-normal);
    font-size: var(--font-base-size);
    color: var(--color-text-primary-light);
  }

  h1,
  h2,
  h3,
  h4,
  h5 {
    color: var(--color-text-primary);
  }

  h1 {
    font: var(--font-h1);
  }

  h2 {
    font: var(--font-h2);
  }

  h3 {
    font: var(--font-h3);
  }

  h4 {
    font: var(--font-h4);
  }

  h5 {
    font: var(--font-h5);
  }

  p {
    font: var(--font-text);
  }

  a {
    font: var(--font-link);
  }

  small {
    font: var(--font-subtext);
  }
`;
