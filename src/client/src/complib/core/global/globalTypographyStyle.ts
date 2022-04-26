import { css } from "styled-components/macro";

/**
 * TYPOGRAPHY SPECIFIC GLOBALS
 */
export const globalTypographyStyle = css`
  body {
    font-family: var(--tl-ref-typeface-brand);
    font-weight: var(--tl-ref-typeface-weight-normal);
    font-size: var(--tl-ref-font-base-size);
    color: var(--tl-sys-color-on-background);
  }

  h1 {
    font: var(--tl-sys-typescale-display-large);
    letter-spacing: var(--tl-sys-typescale-display-large-spacing);
    line-height: var(--tl-sys-typescale-display-large-line-height);
  }

  h2 {
    font: var(--tl-sys-typescale-display-medium);
    letter-spacing: var(--tl-sys-typescale-display-medium-spacing);
    line-height: var(--tl-sys-typescale-display-medium-line-height);
  }

  h3 {
    font: var(--tl-sys-typescale-display-small);
    letter-spacing: var(--tl-sys-typescale-display-small-spacing);
    line-height: var(--tl-sys-typescale-display-small-line-height);
  }

  h4 {
    font: var(--tl-sys-typescale-headline-large);
    letter-spacing: var(--tl-sys-typescale-headline-large-spacing);
    line-height: var(--tl-sys-typescale-headline-large-line-height);
  }

  h5 {
    font: var(--tl-sys-typescale-headline-medium);
    letter-spacing: var(--tl-sys-typescale-headline-medium-spacing);
    line-height: var(--tl-sys-typescale-headline-medium-line-height);
  }

  h6 {
    font: var(--tl-sys-typescale-headline-small);
    letter-spacing: var(--tl-sys-typescale-headline-small-spacing);
    line-height: var(--tl-sys-typescale-headline-small-line-height);
  }

  p,
  a {
    font: var(--tl-sys-typescale-body-large);
    letter-spacing: var(--tl-sys-typescale-body-large-spacing);
    line-height: var(--tl-sys-typescale-body-large-line-height);
  }

  a:hover {
    text-decoration: underline;
    cursor: pointer;
  }

  b,
  strong {
    font-size: var(--tl-sys-typescale-body-large-size);
    font-weight: var(--tl-ref-typeface-weight-bold);
    letter-spacing: var(--tl-sys-typescale-body-large-spacing);
    line-height: var(--tl-sys-typescale-body-large-line-height);
  }

  small {
    font: var(--tl-sys-typescale-body-small);
    letter-spacing: var(--tl-sys-typescale-body-small-spacing);
    line-height: var(--tl-sys-typescale-body-small-line-height);
  }
`;
