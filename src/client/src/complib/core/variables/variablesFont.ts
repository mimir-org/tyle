import { css } from "styled-components/macro";

export const variablesFont = css`
  :root {
    --tl-sys-font-family: "roboto", sans-serif;

    --tl-sys-font-weight-bold: 700;
    --tl-sys-font-weight-normal: 400;
    --tl-sys-font-weight-light: 300;

    --tl-sys-font-base-size: 100%;
    --tl-sys-font-scale-ratio: 1.2;

    --tl-sys-font-size-xxs: calc(var(--tl-sys-font-size-xs) / var(--tl-sys-font-scale-ratio));
    --tl-sys-font-size-xs: calc(var(--tl-sys-font-size-small) / var(--tl-sys-font-scale-ratio));
    --tl-sys-font-size-small: calc(var(--tl-sys-font-size-base) / var(--tl-sys-font-scale-ratio));
    --tl-sys-font-size-base: 1rem;
    --tl-sys-font-size-medium: calc(var(--tl-sys-font-size-base) * var(--tl-sys-font-scale-ratio));
    --tl-sys-font-size-large: calc(var(--tl-sys-font-size-medium) * var(--tl-sys-font-scale-ratio));
    --tl-sys-font-size-xl: calc(var(--tl-sys-font-size-large) * var(--tl-sys-font-scale-ratio));
    --tl-sys-font-size-xxl: calc(var(--tl-sys-font-size-xl) * var(--tl-sys-font-scale-ratio));
    --tl-sys-font-size-xxxl: calc(var(--tl-sys-font-size-xxl) * var(--tl-sys-font-scale-ratio));

    --tl-sys-font-h1: var(--tl-sys-font-weight-bold) var(--tl-sys-font-size-xxxl) var(--tl-sys-font-family);
    --tl-sys-font-h2: var(--tl-sys-font-weight-bold) var(--tl-sys-font-size-xxl) var(--tl-sys-font-family);
    --tl-sys-font-h3: var(--tl-sys-font-weight-light) var(--tl-sys-font-size-xl) var(--tl-sys-font-family);
    --tl-sys-font-h4: var(--tl-sys-font-weight-bold) var(--tl-sys-font-size-large) var(--tl-sys-font-family);
    --tl-sys-font-h5: var(--tl-sys-font-weight-bold) var(--tl-sys-font-size-medium) var(--tl-sys-font-family);
    --tl-sys-font-link: var(--tl-sys-font-weight-normal) var(--tl-sys-font-size-base) var(--tl-sys-font-family);
    --tl-sys-font-text: var(--tl-sys-font-weight-normal) var(--tl-sys-font-size-base) var(--tl-sys-font-family);
    --tl-sys-font-subtext: var(--tl-sys-font-weight-normal) var(--tl-sys-font-size-small) var(--tl-sys-font-family);
  }
`;

export const font = {
  family: "var(--tl-sys-font-family)",
  weights: {
    bold: "var(--tl-sys-font-weight-bold)",
    normal: "var(--tl-sys-font-weight-normal)",
    light: "var(--tl-sys-font-weight-light)",
  },
  sizes: {
    xxs: "var(--tl-sys-font-size-xxs)",
    xs: "var(--tl-sys-font-size-xs)",
    small: "var(--tl-sys-font-size-small)",
    base: "var(--tl-sys-font-size-base)",
    medium: "var(--tl-sys-font-size-medium)",
    large: "var(--tl-sys-font-size-large)",
    xl: "var(--tl-sys-font-size-xl)",
    xxl: "var(--tl-sys-font-size-xxl)",
    xxxl: "var(--tl-sys-font-size-xxxl)",
  },
  types: {
    h1: "var(--tl-sys-font-h1)",
    h2: "var(--tl-sys-font-h2)",
    h3: "var(--tl-sys-font-h3)",
    h4: "var(--tl-sys-font-h4)",
    h6: "var(--tl-sys-font-h5)",
    link: "var(--tl-sys-font-link)",
    text: "var(--tl-sys-font-text)",
    subtext: "var(--tl-sys-font-subtext)",
  },
};
