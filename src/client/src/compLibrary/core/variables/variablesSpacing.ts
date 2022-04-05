import { css } from "styled-components/macro";

export const variablesSpacing = css`
  :root {
    --spacing-xs: 4px;
    --spacing-small: 8px;
    --spacing-medium: 16px;
    --spacing-large: 24px;
    --spacing-xl: 32px;
    --spacing-xxl: 48px;
    --spacing-xxxl: 64px;
  }
`;

export const SPACING = {
  XS: "var(--spacing-xs)",
  SMALL: "var(--spacing-small)",
  MEDIUM: "var(--spacing-medium)",
  LARGE: "var(--spacing-large)",
  XL: "var(--spacing-xl)",
  XXL: "var(--spacing-xxl)",
  XXXL: "var(--spacing-xxxl)",
};
