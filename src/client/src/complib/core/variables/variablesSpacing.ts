import { css } from "styled-components/macro";
import { math } from "polished";

const spacingUnit = "1rem";
export const spacing: SpacingSystem = {
  unit: spacingUnit,
  xxs: math(`${spacingUnit} * 0.25`),
  xs: math(`${spacingUnit} * 0.5`),
  small: math(`${spacingUnit} * 0.75`),
  medium: math(`${spacingUnit} * 1.25`),
  large: math(`${spacingUnit} * 2`),
  xl: math(`${spacingUnit} * 3.25`),
  xxl: math(`${spacingUnit} * 5.25`),
  xxxl: math(`${spacingUnit} * 8.5`),
};

export const variablesSpacing = css`
  :root {
    --tl-sys-spacing-unit: ${spacing.unit};
    --tl-sys-spacing-xxs: ${spacing.xxs};
    --tl-sys-spacing-xs: ${spacing.xs};
    --tl-sys-spacing-small: ${spacing.small};
    --tl-sys-spacing-medium: ${spacing.medium};
    --tl-sys-spacing-large: ${spacing.large};
    --tl-sys-spacing-xl: ${spacing.xl};
    --tl-sys-spacing-xxl: ${spacing.xxl};
    --tl-sys-spacing-xxxl: ${spacing.xxxl};
  }
`;

export interface SpacingSystem {
  unit: string;
  xxs: string;
  xs: string;
  small: string;
  medium: string;
  large: string;
  xl: string;
  xxl: string;
  xxxl: string;
}