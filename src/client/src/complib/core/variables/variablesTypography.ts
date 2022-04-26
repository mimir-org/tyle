import { css } from "styled-components/macro";

export const variablesTypography = css`
  :root {
    --tl-ref-typeface-brand: "roboto", sans-serif;

    --tl-ref-typeface-weight-bold: 700;
    --tl-ref-typeface-weight-medium: 500;
    --tl-ref-typeface-weight-normal: 400;
    --tl-ref-typeface-weight-light: 300;
    --tl-ref-font-base-size: 100%;

    // Font sizes in PX
    --tl-ref-typescale-size-b: 16;
    --tl-ref-typescale-size-n3: 11;
    --tl-ref-typescale-size-n2: 12;
    --tl-ref-typescale-size-n1: 14;
    --tl-ref-typescale-size-p1: 22;
    --tl-ref-typescale-size-p2: 24;
    --tl-ref-typescale-size-p3: 28;
    --tl-ref-typescale-size-p4: 32;
    --tl-ref-typescale-size-p5: 36;
    --tl-ref-typescale-size-p6: 45;
    --tl-ref-typescale-size-p7: 57;

    // Line heights in PX
    --tl-ref-typescale-line-height-n3: 6;
    --tl-ref-typescale-line-height-n2: 16;
    --tl-ref-typescale-line-height-n1: 20;
    --tl-ref-typescale-line-height-b: 24;
    --tl-ref-typescale-line-height-p1: 28;
    --tl-ref-typescale-line-height-p2: 32;
    --tl-ref-typescale-line-height-p3: 36;
    --tl-ref-typescale-line-height-p4: 40;
    --tl-ref-typescale-line-height-p5: 44;
    --tl-ref-typescale-line-height-p6: 52;
    --tl-ref-typescale-line-height-p7: 64;

    // REM converted font sizes
    --tl-sys-typescale-size-b: calc(var(--tl-ref-typescale-size-b) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-size-n3: calc(var(--tl-ref-typescale-size-n3) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-size-n2: calc(var(--tl-ref-typescale-size-n2) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-size-n1: calc(var(--tl-ref-typescale-size-n1) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-size-p1: calc(var(--tl-ref-typescale-size-p1) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-size-p2: calc(var(--tl-ref-typescale-size-p2) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-size-p3: calc(var(--tl-ref-typescale-size-p3) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-size-p4: calc(var(--tl-ref-typescale-size-p4) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-size-p5: calc(var(--tl-ref-typescale-size-p5) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-size-p6: calc(var(--tl-ref-typescale-size-p6) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-size-p7: calc(var(--tl-ref-typescale-size-p7) / var(--tl-ref-typescale-size-b) * 1rem);

    // REM converted line heights
    --tl-sys-typescale-line-height-b: calc(var(--tl-ref-typescale-line-height-b) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-line-height-n3: calc(var(--tl-ref-typescale-line-height-n3) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-line-height-n2: calc(var(--tl-ref-typescale-line-height-n2) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-line-height-n1: calc(var(--tl-ref-typescale-line-height-n1) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-line-height-p1: calc(var(--tl-ref-typescale-line-height-p1) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-line-height-p2: calc(var(--tl-ref-typescale-line-height-p2) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-line-height-p3: calc(var(--tl-ref-typescale-line-height-p3) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-line-height-p4: calc(var(--tl-ref-typescale-line-height-p4) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-line-height-p5: calc(var(--tl-ref-typescale-line-height-p5) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-line-height-p6: calc(var(--tl-ref-typescale-line-height-p6) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-line-height-p7: calc(var(--tl-ref-typescale-line-height-p7) / var(--tl-ref-typescale-size-b) * 1rem);

    // Display types
    --tl-sys-typescale-display-large-tracking: 0;
    --tl-sys-typescale-display-large-size: var(--tl-sys-typescale-size-p7);
    --tl-sys-typescale-display-large-line-height: var(--tl-sys-typescale-line-height-p7);
    --tl-sys-typescale-display-large-weight: var(--tl-ref-typeface-weight-normal);
    --tl-sys-typescale-display-large-font: var(--tl-ref-typeface-brand);
    --tl-sys-typescale-display-large-spacing: calc(var(--tl-sys-typescale-display-large-tracking) / var(--tl-ref-typescale-size-p7) * 1rem);
    --tl-sys-typescale-display-large: var(--tl-sys-typescale-display-large-weight) var(--tl-sys-typescale-display-large-size) var(--tl-sys-typescale-display-large-font);

    --tl-sys-typescale-display-medium-tracking: 0;
    --tl-sys-typescale-display-medium-size: var(--tl-sys-typescale-size-p6);
    --tl-sys-typescale-display-medium-line-height: var(--tl-sys-typescale-line-height-p6);
    --tl-sys-typescale-display-medium-weight: var(--tl-ref-typeface-weight-normal);
    --tl-sys-typescale-display-medium-font: var(--tl-ref-typeface-brand);
    --tl-sys-typescale-display-medium-spacing: calc(var(--tl-sys-typescale-display-medium-tracking) / var(--tl-ref-typescale-size-p6) * 1rem);
    --tl-sys-typescale-display-medium: var(--tl-sys-typescale-display-medium-weight) var(--tl-sys-typescale-display-medium-size) var(--tl-sys-typescale-display-medium-font);

    --tl-sys-typescale-display-small-tracking: 0;
    --tl-sys-typescale-display-small-size: var(--tl-sys-typescale-size-p5);
    --tl-sys-typescale-display-small-line-height: var(--tl-sys-typescale-line-height-p5);
    --tl-sys-typescale-display-small-weight: var(--tl-ref-typeface-weight-normal);
    --tl-sys-typescale-display-small-font: var(--tl-ref-typeface-brand);
    --tl-sys-typescale-display-small-spacing: calc(var(--tl-sys-typescale-display-small-tracking) / var(--tl-ref-typescale-size-p5) * 1rem);
    --tl-sys-typescale-display-small: var(--tl-sys-typescale-display-small-weight) var(--tl-sys-typescale-display-small-size) var(--tl-sys-typescale-display-small-font);

    // Headline types
    --tl-sys-typescale-headline-large-tracking: 0;
    --tl-sys-typescale-headline-large-size: var(--tl-sys-typescale-size-p4);
    --tl-sys-typescale-headline-large-line-height: var(--tl-sys-typescale-line-height-p4);
    --tl-sys-typescale-headline-large-weight: var(--tl-ref-typeface-weight-normal);
    --tl-sys-typescale-headline-large-font: var(--tl-ref-typeface-brand);
    --tl-sys-typescale-headline-large-spacing: calc(var(--tl-sys-typescale-headline-large-tracking) / var(--tl-ref-typescale-size-p4) * 1rem);
    --tl-sys-typescale-headline-large: var(--tl-sys-typescale-headline-large-weight) var(--tl-sys-typescale-headline-large-size) var(--tl-sys-typescale-headline-large-font);

    --tl-sys-typescale-headline-medium-tracking: 0;
    --tl-sys-typescale-headline-medium-size: var(--tl-sys-typescale-size-p3);
    --tl-sys-typescale-headline-medium-line-height: var(--tl-sys-typescale-line-height-p3);
    --tl-sys-typescale-headline-medium-weight: var(--tl-ref-typeface-weight-normal);
    --tl-sys-typescale-headline-medium-font: var(--tl-ref-typeface-brand);
    --tl-sys-typescale-headline-medium-spacing: calc(var(-tl-sys-typescale-display-large-tracking) / var(--tl-ref-typescale-size-p3) * 1rem);
    --tl-sys-typescale-headline-medium: var(--tl-sys-typescale-headline-medium-weight) var(--tl-sys-typescale-headline-medium-size) var(--tl-sys-typescale-headline-medium-font);

    --tl-sys-typescale-headline-small-tracking: 0;
    --tl-sys-typescale-headline-small-size: var(--tl-sys-typescale-size-p2);
    --tl-sys-typescale-headline-small-line-height: var(--tl-sys-typescale-line-height-p2);
    --tl-sys-typescale-headline-small-weight: var(--tl-ref-typeface-weight-normal);
    --tl-sys-typescale-headline-small-font: var(--tl-ref-typeface-brand);
    --tl-sys-typescale-headline-small-spacing: calc(var(--tl-sys-typescale-title-small-tracking) / var(--tl-ref-typescale-size-p2) * 1rem);
    --tl-sys-typescale-headline-small: var(--tl-sys-typescale-headline-small-weight) var(--tl-sys-typescale-headline-small-size) var(--tl-sys-typescale-headline-small-font);

    // Title types
    --tl-sys-typescale-title-large-tracking: 0;
    --tl-sys-typescale-title-large-size: var(--tl-sys-typescale-size-p1);
    --tl-sys-typescale-title-large-line-height: var(--tl-sys-typescale-line-height-p1);
    --tl-sys-typescale-title-large-weight: var(--tl-ref-typeface-weight-normal);
    --tl-sys-typescale-title-large-font: var(--tl-ref-typeface-brand);
    --tl-sys-typescale-title-large-spacing: calc(var(--tl-sys-typescale-title-large-tracking) / var(--tl-ref-typescale-size-p1) * 1rem);
    --tl-sys-typescale-title-large: var(--tl-sys-typescale-title-large-weight) var(--tl-sys-typescale-title-large-size) var(--tl-sys-typescale-title-large-font);

    --tl-sys-typescale-title-medium-tracking: 0.15;
    --tl-sys-typescale-title-medium-size: var(--tl-sys-typescale-size-b);
    --tl-sys-typescale-title-medium-line-height: var(--tl-sys-typescale-line-height-b);
    --tl-sys-typescale-title-medium-weight: var(--tl-ref-typeface-weight-medium);
    --tl-sys-typescale-title-medium-font: var(--tl-ref-typeface-brand);
    --tl-sys-typescale-title-medium-spacing: calc(var(--tl-sys-typescale-title-medium-tracking) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-title-medium: var(--tl-sys-typescale-title-medium-weight) var(--tl-sys-typescale-title-medium-size) var(--tl-sys-typescale-title-medium-font);

    --tl-sys-typescale-title-small-tracking: 0.1;
    --tl-sys-typescale-title-small-size: var(--tl-sys-typescale-size-n1);
    --tl-sys-typescale-title-small-line-height: var(--tl-sys-typescale-line-height-n1);
    --tl-sys-typescale-title-small-weight: var(--tl-ref-typeface-weight-medium);
    --tl-sys-typescale-title-small-font: var(--tl-ref-typeface-brand);
    --tl-sys-typescale-title-small-spacing: calc(var(--tl-sys-typescale-title-small-tracking) / var(--tl-ref-typescale-size-n1) * 1rem);
    --tl-sys-typescale-title-small: var(--tl-sys-typescale-title-small-weight) var(--tl-sys-typescale-title-small-size) var(--tl-sys-typescale-title-small-font);

    // Body types

    --tl-sys-typescale-body-large-tracking: 0.1;
    --tl-sys-typescale-body-large-size: var(--tl-sys-typescale-size-b);
    --tl-sys-typescale-body-large-line-height: var(--tl-sys-typescale-line-height-b);
    --tl-sys-typescale-body-large-weight: var(--tl-ref-typeface-weight-normal);
    --tl-sys-typescale-body-large-font: var(--tl-ref-typeface-brand);
    --tl-sys-typescale-body-large-spacing: calc(var(--tl-sys-typescale-body-large) / var(--tl-ref-typescale-size-b) * 1rem);
    --tl-sys-typescale-body-large: var(--tl-sys-typescale-body-large-weight) var(--tl-sys-typescale-body-large-size) var(--tl-sys-typescale-body-large-font);

    --tl-sys-typescale-body-medium-tracking: 0;
    --tl-sys-typescale-body-medium-size: var(--tl-sys-typescale-size-n1);
    --tl-sys-typescale-body-medium-line-height: var(--tl-sys-typescale-line-height-n1);
    --tl-sys-typescale-body-medium-weight: var(--tl-ref-typeface-weight-normal);
    --tl-sys-typescale-body-medium-font: var(--tl-ref-typeface-brand);
    --tl-sys-typescale-body-medium-spacing: calc(var(--tl-sys-typescale-body-medium-tracking) / var(--tl-ref-typescale-size-n1) * 1rem);
    --tl-sys-typescale-body-medium: var(--tl-sys-typescale-body-medium-weight) var(--tl-sys-typescale-body-medium-size) var(--tl-sys-typescale-body-medium-font);

    --tl-sys-typescale-body-small-tracking: 0;
    --tl-sys-typescale-body-small-size: var(--tl-sys-typescale-size-n2);
    --tl-sys-typescale-body-small-line-height: var(--tl-sys-typescale-line-height-n2);
    --tl-sys-typescale-body-small-weight: var(--tl-ref-typeface-weight-normal);
    --tl-sys-typescale-body-small-font: var(--tl-ref-typeface-brand);
    --tl-sys-typescale-body-small-spacing: calc(var(--tl-sys-typescale-body-small-tracking) / var(--tl-ref-typescale-size-n2) * 1rem);
    --tl-sys-typescale-body-small: var(--tl-sys-typescale-body-small-weight) var(--tl-sys-typescale-body-small-size) var(--tl-sys-typescale-body-small-font);

    // Label types
    --tl-sys-typescale-label-large-tracking: 0.1;
    --tl-sys-typescale-label-large-size: var(--tl-sys-typescale-size-n1);
    --tl-sys-typescale-label-large-line-height: var(--tl-sys-typescale-line-height-n1);
    --tl-sys-typescale-label-large-weight: var(--tl-ref-typeface-weight-medium);
    --tl-sys-typescale-label-large-font: var(--tl-ref-typeface-brand);
    --tl-sys-typescale-label-large-spacing: calc(var(--tl-sys-typescale-label-large-tracking) / var(--tl-ref-typescale-size-n1) * 1rem);
    --tl-sys-typescale-label-large: var(--tl-sys-typescale-label-large-weight) var(--tl-sys-typescale-label-large-size) var(--tl-sys-typescale-label-large-font);

    --tl-sys-typescale-label-medium-tracking: 0.5;
    --tl-sys-typescale-label-medium-size: var(--tl-sys-typescale-size-n2);
    --tl-sys-typescale-label-medium-line-height: var(--tl-sys-typescale-line-height-n2);
    --tl-sys-typescale-label-medium-weight: var(--tl-ref-typeface-weight-medium);
    --tl-sys-typescale-label-medium-font: var(--tl-ref-typeface-brand);
    --tl-sys-typescale-label-medium-spacing: calc(var(--tl-sys-typescale-label-medium-tracking) / var(--tl-ref-typescale-size-n2) * 1rem);
    --tl-sys-typescale-label-medium: var(--tl-sys-typescale-label-medium-weight) var(--tl-sys-typescale-label-medium-size) var(--tl-sys-typescale-label-medium-font);

    --tl-sys-typescale-label-small-tracking: 0.5;
    --tl-sys-typescale-label-small-size: var(--tl-sys-typescale-size-n3);
    --tl-sys-typescale-label-small-line-height: var(--tl-sys-typescale-line-height-n3);
    --tl-sys-typescale-label-small-weight: var(--tl-ref-typeface-weight-medium);
    --tl-sys-typescale-label-small-font: var(--tl-ref-typeface-brand);
    --tl-sys-typescale-label-small-spacing: calc(var(--tl-sys-typescale-label-small-tracking) / var(--tl-ref-typescale-size-n3) * 1rem);
    --tl-sys-typescale-label-small: var(--tl-sys-typescale-label-small-weight) var(--tl-sys-typescale-label-small-size) var(--tl-sys-typescale-label-small-font);
  }
`;

export const typography = {
  display: {
    large: {
      font: "var(--tl-sys-typescale-display-large)",
      size: "var(--tl-sys-typescale-display-large-size)",
      weight: "var(--tl-sys-typescale-display-large-weight)",
      fontFamily: "var(--tl-sys-typescale-display-large-font)",
      tracking: "var(--tl-sys-typescale-display-large-tracking)",
      letterSpacing: "var(--tl-sys-typescale-display-large-spacing)",
      lineHeight: "var(--tl-sys-typescale-display-large-line-height)",
    },
    medium: {
      font: "var(--tl-sys-typescale-display-medium)",
      size: "var(--tl-sys-typescale-display-medium-size)",
      weight: "var(--tl-sys-typescale-display-medium-weight)",
      fontFamily: "var(--tl-sys-typescale-display-medium-font)",
      tracking: "var(--tl-sys-typescale-display-medium-tracking)",
      letterSpacing: "var(--tl-sys-typescale-display-medium-spacing)",
      lineHeight: "var(--tl-sys-typescale-display-medium-line-height)",
    },
    small: {
      font: "var(--tl-sys-typescale-display-small)",
      size: "var(--tl-sys-typescale-display-small-size)",
      weight: "var(--tl-sys-typescale-display-small-weight)",
      fontFamily: "var(--tl-sys-typescale-display-small-font)",
      tracking: "var(--tl-sys-typescale-display-small-tracking)",
      letterSpacing: "var(--tl-sys-typescale-display-small-spacing)",
      lineHeight: "var(--tl-sys-typescale-display-small-line-height)",
    },
  },
  headline: {
    large: {
      font: "var(--tl-sys-typescale-headline-large)",
      size: "var(--tl-sys-typescale-headline-large-size)",
      weight: "var(--tl-sys-typescale-headline-large-weight)",
      fontFamily: "var(--tl-sys-typescale-headline-large-font)",
      tracking: "var(--tl-sys-typescale-headline-large-tracking)",
      letterSpacing: "var(--tl-sys-typescale-headline-large-spacing)",
      lineHeight: "var(--tl-sys-typescale-headline-large-line-height)",
    },
    medium: {
      font: "var(--tl-sys-typescale-headline-medium)",
      size: "var(--tl-sys-typescale-headline-medium-size)",
      weight: "var(--tl-sys-typescale-headline-medium-weight)",
      fontFamily: "var(--tl-sys-typescale-headline-medium-font)",
      tracking: "var(--tl-sys-typescale-headline-medium-tracking)",
      letterSpacing: "var(--tl-sys-typescale-headline-medium-spacing)",
      lineHeight: "var(--tl-sys-typescale-headline-medium-line-height)",
    },
    small: {
      font: "var(--tl-sys-typescale-headline-small)",
      size: "var(--tl-sys-typescale-headline-small-size)",
      weight: "var(--tl-sys-typescale-headline-small-weight)",
      fontFamily: "var(--tl-sys-typescale-headline-small-font)",
      tracking: "var(--tl-sys-typescale-headline-small-tracking)",
      letterSpacing: "var(--tl-sys-typescale-headline-small-spacing)",
      lineHeight: "var(--tl-sys-typescale-headline-small-line-height)",
    },
  },
  title: {
    large: {
      font: "var(--tl-sys-typescale-title-large)",
      size: "var(--tl-sys-typescale-title-large-size)",
      weight: "var(--tl-sys-typescale-title-large-weight)",
      fontFamily: "var(--tl-sys-typescale-title-large-font)",
      tracking: "var(--tl-sys-typescale-title-large-tracking)",
      letterSpacing: "var(--tl-sys-typescale-title-large-spacing)",
      lineHeight: "var(--tl-sys-typescale-title-large-line-height)",
    },
    medium: {
      font: "var(--tl-sys-typescale-title-medium)",
      size: "var(--tl-sys-typescale-title-medium-size)",
      weight: "var(--tl-sys-typescale-title-medium-weight)",
      fontFamily: "var(--tl-sys-typescale-title-medium-font)",
      tracking: "var(--tl-sys-typescale-title-medium-tracking)",
      letterSpacing: "var(--tl-sys-typescale-title-medium-spacing)",
      lineHeight: "var(--tl-sys-typescale-title-medium-line-height)",
    },
    small: {
      font: "var(--tl-sys-typescale-title-small)",
      size: "var(--tl-sys-typescale-title-small-size)",
      weight: "var(--tl-sys-typescale-title-small-weight)",
      fontFamily: "var(--tl-sys-typescale-title-small-font)",
      tracking: "var(--tl-sys-typescale-title-small-tracking)",
      letterSpacing: "var(--tl-sys-typescale-title-small-spacing)",
      lineHeight: "var(--tl-sys-typescale-title-small-line-height)",
    },
  },
  body: {
    large: {
      font: "var(--tl-sys-typescale-body-large)",
      size: "var(--tl-sys-typescale-body-large-size)",
      weight: "var(--tl-sys-typescale-body-large-weight)",
      fontFamily: "var(--tl-sys-typescale-body-large-font)",
      tracking: "var(--tl-sys-typescale-body-large-tracking)",
      letterSpacing: "var(--tl-sys-typescale-body-large-spacing)",
      lineHeight: "var(--tl-sys-typescale-body-large-line-height)",
    },
    medium: {
      font: "var(--tl-sys-typescale-body-medium)",
      size: "var(--tl-sys-typescale-body-medium-size)",
      weight: "var(--tl-sys-typescale-body-medium-weight)",
      fontFamily: "var(--tl-sys-typescale-body-medium-font)",
      tracking: "var(--tl-sys-typescale-body-medium-tracking)",
      letterSpacing: "var(--tl-sys-typescale-body-medium-spacing)",
      lineHeight: "var(--tl-sys-typescale-body-medium-line-height)",
    },
    small: {
      font: "var(--tl-sys-typescale-body-small)",
      size: "var(--tl-sys-typescale-body-small-size)",
      weight: "var(--tl-sys-typescale-body-small-weight)",
      fontFamily: "var(--tl-sys-typescale-body-small-font)",
      tracking: "var(--tl-sys-typescale-body-small-tracking)",
      letterSpacing: "var(--tl-sys-typescale-body-small-spacing)",
      lineHeight: "var(--tl-sys-typescale-body-small-line-height)",
    },
  },
  label: {
    large: {
      font: "var(--tl-sys-typescale-label-large)",
      size: "var(--tl-sys-typescale-label-large-size)",
      weight: "var(--tl-sys-typescale-label-large-weight)",
      fontFamily: "var(--tl-sys-typescale-label-large-font)",
      tracking: "var(--tl-sys-typescale-label-large-tracking)",
      letterSpacing: "var(--tl-sys-typescale-label-large-spacing)",
      lineHeight: "var(--tl-sys-typescale-label-large-line-height)",
    },
    medium: {
      font: "var(--tl-sys-typescale-label-medium)",
      size: "var(--tl-sys-typescale-label-medium-size)",
      weight: "var(--tl-sys-typescale-label-medium-weight)",
      fontFamily: "var(--tl-sys-typescale-label-medium-font)",
      tracking: "var(--tl-sys-typescale-label-medium-tracking)",
      letterSpacing: "var(--tl-sys-typescale-label-medium-spacing)",
      lineHeight: "var(--tl-sys-typescale-label-medium-line-height)",
    },
    small: {
      font: "var(--tl-sys-typescale-label-small)",
      size: "var(--tl-sys-typescale-label-small-size)",
      weight: "var(--tl-sys-typescale-label-small-weight)",
      fontFamily: "var(--tl-sys-typescale-label-small-font)",
      tracking: "var(--tl-sys-typescale-label-small-tracking)",
      letterSpacing: "var(--tl-sys-typescale-label-small-spacing)",
      lineHeight: "var(--tl-sys-typescale-label-small-line-height)",
    },
  },
};
