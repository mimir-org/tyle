import { css } from "styled-components/macro";

export const variablesSpacing = css`
  :root {
    --spacing-unit: 1rem;
    --spacing-xxs: calc(0.25 * var(--spacing-unit));
    --spacing-xs: calc(0.5 * var(--spacing-unit));
    --spacing-small: calc(0.75 * var(--spacing-unit));
    --spacing-medium: calc(1.25 * var(--spacing-unit));
    --spacing-large: calc(2 * var(--spacing-unit));
    --spacing-xl: calc(3.25 * var(--spacing-unit));
    --spacing-xxl: calc(5.25 * var(--spacing-unit));
    --spacing-xxxl: calc(8.5 * var(--spacing-unit));
  }
`;

export const SPACING = {
  XXS: "var(--spacing-xxs)",
  XS: "var(--spacing-xs)",
  SMALL: "var(--spacing-small)",
  MEDIUM: "var(--spacing-medium)",
  LARGE: "var(--spacing-large)",
  XL: "var(--spacing-xl)",
  XXL: "var(--spacing-xxl)",
  XXXL: "var(--spacing-xxxl)",
};
