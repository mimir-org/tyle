import { css } from "styled-components/macro";

export const variablesColor = css`
  :root {
    --color-primary: hsl(240, 17%, 19%);
    --color-primary-light: hsl(240, 22%, 70%);
    --color-primary-dark: hsl(240, 33%, 12%);
    --color-primary-alpha: hsla(240, 22%, 70%, 0.5);

    --color-secondary: hsl(304, 19%, 32%);
    --color-secondary-light: hsl(315, 29%, 73%);
    --color-secondary-dark: hsl(293, 35%, 20%);
    --color-secondary-alpha: hsla(315, 29%, 73%, 0.5);

    --color-surface-primary: hsl(0, 0%, 98%);

    --color-background-primary: hsl(0, 0%, 100%);
    --color-background-primary-inverted: hsl(0, 0%, 0%);

    --color-text-primary: hsl(0, 0%, 0%);
    --color-text-primary-inverted: hsl(0, 0%, 100%);
    --color-text-secondary: hsl(0, 0%, 55%);

    --color-border-primary: hsl(243, 12%, 70%);
    --color-border-primary-light: hsl(244, 25%, 89%);

    --color-positive: hsl(102, 70%, 29%);
    --color-positive-dark: hsl(110, 78%, 20%);
    --color-positive-light: hsl(94, 58%, 64%);

    --color-negative: hsl(331, 80%, 34%);
    --color-negative-dark: hsl(319, 87%, 23%);
    --color-negative-light: hsl(343, 67%, 65%);

    --color-info: hsl(183, 100%, 24%);
    --color-info-dark: hsl(193, 100%, 17%);
    --color-info-light: hsl(173, 62%, 59%);

    --color-alert: hsl(49, 99%, 35%);
    --color-alert-dark: hsl(47, 100%, 25%);
    --color-alert-light: hsl(52, 74%, 64%);
  }
`;

export const COLOR = {
  PRIMARY: {
    BASE: "var(--color-primary)",
    DARK: "var(--color-primary-dark)",
    LIGHT: "var(--color-primary-light)",
    ALPHA: "var(--color-primary-alpha)",
  },
  SECONDARY: {
    BASE: "var(--color-secondary)",
    DARK: "var(--color-secondary-dark)",
    LIGHT: "var(--color-secondary-light)",
    ALPHA: "var(--color-secondary-alpha)",
  },
  SEMANTIC: {
    POSITIVE: {
      BASE: "var(--color-positive)",
      DARK: "var(--color-positive-dark)",
      LIGHT: "var(--color-positive-light)",
    },
    NEGATIVE: {
      BASE: "var(--color-negative)",
      DARK: "var(--color-negative-dark)",
      LIGHT: "var(--color-negative-light)",
    },
    ALERT: {
      BASE: "var(--color-alert)",
      DARK: "var(--color-alert-dark)",
      LIGHT: "var(--color-alert-light)",
    },
    INFO: {
      BASE: "var(--color-info)",
      DARK: "var(--color-info-dark)",
      LIGHT: "var(--color-info-light)",
    },
  },
  SURFACE: {
    PRIMARY: {
      BASE: "var(--color-surface-primary)",
    },
  },
  BACKGROUND: {
    PRIMARY: {
      BASE: "var(--color-background-primary)",
      INVERTED: "var(--color-background-primary-inverted)",
    },
  },
  TEXT: {
    PRIMARY: {
      BASE: "var(--color-text-primary)",
      INVERTED: "var(--color-text-primary-inverted)",
    },
    SECONDARY: {
      BASE: "var(--color-text-secondary)",
    },
  },
  BORDER: {
    PRIMARY: {
      BASE: "var(--color-border-primary)",
      LIGHT: "var(--color-border-primary-light)",
    },
  },
};
