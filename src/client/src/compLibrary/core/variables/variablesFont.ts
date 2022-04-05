import { css } from "styled-components/macro";

export const variablesFont = css`
  :root {
    --font-family: "roboto";

    --font-weight-bold: 700;
    --font-weight-normal: 400;
    --font-weight-light: 300;

    /* 1.200 - Minor third scale */
    --font-size: 100%; /*16px*/
    --font-scale: 1.2;

    --font-size-xs: calc(var(--font-size-small) / var(--font-scale));
    --font-size-small: calc(var(--font-size-base) / var(--font-scale));
    --font-size-base: 1rem;
    --font-size-medium: calc(var(--font-size-base) * var(--font-scale));
    --font-size-large: calc(var(--font-size-medium) * var(--font-scale));
    --font-size-xl: calc(var(--font-size-large) * var(--font-scale));
    --font-size-xxl: calc(var(--font-size-xl) * var(--font-scale));
    --font-size-xxxl: calc(var(--font-size-xxl) * var(--font-scale));

    --font-h1: var(--font-weight-bold) var(--font-size-xxxl) var(--font-family);
    --font-h2: var(--font-weight-bold) var(--font-size-xxl) var(--font-family);
    --font-h3: var(--font-weight-light) var(--font-size-xl) var(--font-family);
    --font-h4: var(--font-weight-bold) var(--font-size-large) var(--font-family);
    --font-h5: var(--font-weight-bold) var(--font-size-medium) var(--font-family);
    --font-link: var(--font-weight-normal) var(--font-size-base) var(--font-family);
    --font-text: var(--font-weight-normal) var(--font-size-base) var(--font-family);
    --font-subtext: var(--font-weight-normal) var(--font-size-small) var(--font-family);
  }
`;

export const FONT = {
  FAMILY: "var(--font-family)",
  WEIGHTS: {
    BOLD: "var(--font-weight-bold)",
    NORMAL: "var(--font-weight-normal)",
    LIGHT: "var(--font-weight-light)",
  },
  SIZES: {
    XS: "var(--font-size-xs)",
    SMALL: "var(--font-size-small)",
    BASE: "var(--font-size-base)",
    MEDIUM: "var(--font-size-medium)",
    LARGE: "var(--font-size-large)",
    XL: "var(--font-size-xl)",
    XXL: "var(--font-size-xxl)",
    XXXL: "var(--font-size-xxxl)",
  },
  TYPES: {
    H1: "var(--font-h1)",
    H2: "var(--font-h2)",
    H3: "var(--font-h3)",
    H4: "var(--font-h4)",
    H5: "var(--font-h5)",
    LINK: "var(--font-link)",
    TEXT: "var(--font-text)",
    SUBTEXT: "var(--font-subText)",
  },
};
