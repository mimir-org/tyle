import { css } from "styled-components/macro";

export const variablesSpacing = css`
  :root {
    --tl-sys-spacing-unit: 1rem;
    --tl-sys-spacing-xxs: calc(0.25 * var(--tl-sys-spacing-unit));
    --tl-sys-spacing-xs: calc(0.5 * var(--tl-sys-spacing-unit));
    --tl-sys-spacing-small: calc(0.75 * var(--tl-sys-spacing-unit));
    --tl-sys-spacing-medium: calc(1.25 * var(--tl-sys-spacing-unit));
    --tl-sys-spacing-large: calc(2 * var(--tl-sys-spacing-unit));
    --tl-sys-spacing-xl: calc(3.25 * var(--tl-sys-spacing-unit));
    --tl-sys-spacing-xxl: calc(5.25 * var(--tl-sys-spacing-unit));
    --tl-sys-spacing-xxxl: calc(8.5 * var(--tl-sys-spacing-unit));
  }
`;

export const spacing: SpacingSystem = {
  xxs: "var(--tl-sys-spacing-xxs)",
  xs: "var(--tl-sys-spacing-xs)",
  small: "var(--tl-sys-spacing-small)",
  medium: "var(--tl-sys-spacing-medium)",
  large: "var(--tl-sys-spacing-large)",
  xl: "var(--tl-sys-spacing-xl)",
  xxl: "var(--tl-sys-spacing-xxl)",
  xxxl: "var(--tl-sys-spacing-xxxl)",
};

export interface SpacingSystem {
  xxs: string;
  xs: string;
  small: string;
  medium: string;
  large: string;
  xl: string;
  xxl: string;
  xxxl: string;
}