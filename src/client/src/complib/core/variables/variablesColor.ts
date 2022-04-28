import { css } from "styled-components/macro";

export const lightTheme: ColorSystem = {
  primary: {
    base: "#5d6408",
    on: "#ffffff",
    container: "#e1eb7b",
    onContainer: "#1a1e01",
  },
  secondary: {
    base: "#5f6145",
    on: "#ffffff",
    container: "#e3e5c3",
    onContainer: "#1b1d08",
  },
  tertiary: {
    base: "#4c665b",
    on: "#ffffff",
    container: "#ceecde",
    onContainer: "#0f2019",
  },
  error: {
    base: "#9d1714",
    on: "#ffffff",
    container: "#f4dad4",
    onContainer: "#350000",
  },
  outline: {
    base: "#78786a",
  },
  background: {
    base: "#fefcf4",
    on: "#1c1c17",
  },
  surface: {
    base: "#fefcf4",
    on: "#1c1c17",
    variant: {
      base: "#e4e3d2",
      on: "#47483b",
    },
    inverse: {
      base: "#31312b",
      on: "#f4f1e9",
    },
  },
}

export const darkTheme: ColorSystem = {
  primary: {
    base: "#c5cf62",
    on: "#2f3402",
    container: "#454b04",
    onContainer: "#e1eb7b",
  },
  secondary: {
    base: "#c7c9a8",
    on: "#30321b",
    container: "#47492f",
    onContainer: "#e3e5c3",
  },
  tertiary: {
    base: "#b3d0c3",
    on: "#1f372d",
    container: "#354e43",
    onContainer: "#ceecde",
  },
  error: {
    base: "#eab3a8",
    on: "#560000",
    container: "#7b0000",
    onContainer: "#f4dad4",
  },
  outline: {
    base: "#929283",
  },
  background: {
    base: "#1c1c17",
    on: "#e4e2da",
  },
  surface: {
    base: "#1c1c17",
    on: "#e4e2da",
    variant: {
      base: "#47483b",
      on: "#c8c7b7",
    },
    inverse: {
      base: "#e5e2da",
      on: "#1c1c17",
    },
  },
}

export const color = {
  light: lightTheme,
  dark: darkTheme,
};

export const variablesColor = css`
  :root {
    // Light    
    --tl-sys-color-primary-light: ${lightTheme.primary.base};
    --tl-sys-color-on-primary-light: ${lightTheme.primary.on};
    --tl-sys-color-primary-container-light: ${lightTheme.primary.container};
    --tl-sys-color-on-primary-container-light: ${lightTheme.primary.onContainer};
    --tl-sys-color-secondary-light: ${lightTheme.secondary.base};
    --tl-sys-color-on-secondary-light: ${lightTheme.secondary.on};
    --tl-sys-color-secondary-container-light: ${lightTheme.secondary.container};
    --tl-sys-color-on-secondary-container-light: ${lightTheme.secondary.onContainer};
    --tl-sys-color-tertiary-light: ${lightTheme.tertiary.base};
    --tl-sys-color-on-tertiary-light: ${lightTheme.tertiary.on};
    --tl-sys-color-tertiary-container-light: ${lightTheme.tertiary.container};
    --tl-sys-color-on-tertiary-container-light: ${lightTheme.tertiary.onContainer};
    --tl-sys-color-error-light: ${lightTheme.error.base};
    --tl-sys-color-on-error-light: ${lightTheme.error.on};
    --tl-sys-color-error-container-light: ${lightTheme.error.container};
    --tl-sys-color-on-error-container-light: ${lightTheme.error.onContainer};
    --tl-sys-color-outline-light: ${lightTheme.outline.base};
    --tl-sys-color-background-light: ${lightTheme.background.base};
    --tl-sys-color-on-background-light: ${lightTheme.background.on};
    --tl-sys-color-surface-light: ${lightTheme.surface.base};
    --tl-sys-color-on-surface-light: ${lightTheme.surface.on};
    --tl-sys-color-surface-variant-light: ${lightTheme.surface.variant.base};
    --tl-sys-color-on-surface-variant-light: ${lightTheme.surface.variant.on};
    --tl-sys-color-inverse-surface-light: ${lightTheme.surface.inverse.base};
    --tl-sys-color-inverse-on-surface-light: ${lightTheme.surface.inverse.on};

    // Dark
    --tl-sys-color-primary-dark: ${darkTheme.primary.base};
    --tl-sys-color-on-primary-dark: ${darkTheme.primary.on};
    --tl-sys-color-primary-container-dark: ${darkTheme.primary.container};
    --tl-sys-color-on-primary-container-dark: ${darkTheme.primary.onContainer};
    --tl-sys-color-secondary-dark: ${darkTheme.secondary.base};
    --tl-sys-color-on-secondary-dark: ${darkTheme.secondary.on};
    --tl-sys-color-secondary-container-dark: ${darkTheme.secondary.container};
    --tl-sys-color-on-secondary-container-dark: ${darkTheme.secondary.onContainer};
    --tl-sys-color-tertiary-dark: ${darkTheme.tertiary.base};
    --tl-sys-color-on-tertiary-dark: ${darkTheme.tertiary.on};
    --tl-sys-color-tertiary-container-dark: ${darkTheme.tertiary.container};
    --tl-sys-color-on-tertiary-container-dark: ${darkTheme.tertiary.onContainer};
    --tl-sys-color-error-dark: ${darkTheme.error.base};
    --tl-sys-color-on-error-dark: ${darkTheme.error.on};
    --tl-sys-color-error-container-dark: ${darkTheme.error.container};
    --tl-sys-color-on-error-container-dark: ${darkTheme.error.onContainer};
    --tl-sys-color-outline-dark: ${darkTheme.outline.base};
    --tl-sys-color-background-dark: ${darkTheme.background.base};
    --tl-sys-color-on-background-dark: ${darkTheme.background.on};
    --tl-sys-color-surface-dark: ${darkTheme.surface.base};
    --tl-sys-color-on-surface-dark: ${darkTheme.surface.on};
    --tl-sys-color-surface-variant-dark: ${darkTheme.surface.variant.base};
    --tl-sys-color-on-surface-variant-dark: ${darkTheme.surface.variant.on};
    --tl-sys-color-inverse-surface-dark: ${darkTheme.surface.inverse.base};
    --tl-sys-color-inverse-on-surface-dark: ${darkTheme.surface.inverse.on};

    @media (prefers-color-scheme: light) {
        --tl-sys-color-primary: var(--tl-sys-color-primary-light);
        --tl-sys-color-on-primary: var(--tl-sys-color-on-primary-light);
        --tl-sys-color-primary-container: var(--tl-sys-color-primary-container-light);
        --tl-sys-color-on-primary-container: var(--tl-sys-color-on-primary-container-light);
        --tl-sys-color-secondary: var(--tl-sys-color-secondary-light);
        --tl-sys-color-on-secondary: var(--tl-sys-color-on-secondary-light);
        --tl-sys-color-secondary-container: var(--tl-sys-color-secondary-container-light);
        --tl-sys-color-on-secondary-container: var(--tl-sys-color-on-secondary-container-light);
        --tl-sys-color-tertiary: var(--tl-sys-color-tertiary-light);
        --tl-sys-color-on-tertiary: var(--tl-sys-color-on-tertiary-light);
        --tl-sys-color-tertiary-container: var(--tl-sys-color-tertiary-container-light);
        --tl-sys-color-on-tertiary-container: var(--tl-sys-color-on-tertiary-container-light);
        --tl-sys-color-error: var(--tl-sys-color-error-light);
        --tl-sys-color-on-error: var(--tl-sys-color-on-error-light);
        --tl-sys-color-error-container: var(--tl-sys-color-error-container-light);
        --tl-sys-color-on-error-container: var(--tl-sys-color-on-error-container-light);
        --tl-sys-color-outline: var(--tl-sys-color-outline-light);
        --tl-sys-color-background: var(--tl-sys-color-background-light);
        --tl-sys-color-on-background: var(--tl-sys-color-on-background-light);
        --tl-sys-color-surface: var(--tl-sys-color-surface-light);
        --tl-sys-color-on-surface: var(--tl-sys-color-on-surface-light);
        --tl-sys-color-surface-variant: var(--tl-sys-color-surface-variant-light);
        --tl-sys-color-on-surface-variant: var(--tl-sys-color-on-surface-variant-light);
        --tl-sys-color-inverse-surface: var(--tl-sys-color-inverse-surface-light);
        --tl-sys-color-inverse-on-surface: var(--tl-sys-color-inverse-on-surface-light);
      
      .dark-theme {
        --tl-sys-color-primary: var(--tl-sys-color-primary-dark);
        --tl-sys-color-on-primary: var(--tl-sys-color-on-primary-dark);
        --tl-sys-color-primary-container: var(--tl-sys-color-primary-container-dark);
        --tl-sys-color-on-primary-container: var(--tl-sys-color-on-primary-container-dark);
        --tl-sys-color-secondary: var(--tl-sys-color-secondary-dark);
        --tl-sys-color-on-secondary: var(--tl-sys-color-on-secondary-dark);
        --tl-sys-color-secondary-container: var(--tl-sys-color-secondary-container-dark);
        --tl-sys-color-on-secondary-container: var(--tl-sys-color-on-secondary-container-dark);
        --tl-sys-color-tertiary: var(--tl-sys-color-tertiary-dark);
        --tl-sys-color-on-tertiary: var(--tl-sys-color-on-tertiary-dark);
        --tl-sys-color-tertiary-container: var(--tl-sys-color-tertiary-container-dark);
        --tl-sys-color-on-tertiary-container: var(--tl-sys-color-on-tertiary-container-dark);
        --tl-sys-color-error: var(--tl-sys-color-error-dark);
        --tl-sys-color-on-error: var(--tl-sys-color-on-error-dark);
        --tl-sys-color-error-container: var(--tl-sys-color-error-container-dark);
        --tl-sys-color-on-error-container: var(--tl-sys-color-on-error-container-dark);
        --tl-sys-color-outline: var(--tl-sys-color-outline-dark);
        --tl-sys-color-background: var(--tl-sys-color-background-dark);
        --tl-sys-color-on-background: var(--tl-sys-color-on-background-dark);
        --tl-sys-color-surface: var(--tl-sys-color-surface-dark);
        --tl-sys-color-on-surface: var(--tl-sys-color-on-surface-dark);
        --tl-sys-color-surface-variant: var(--tl-sys-color-surface-variant-dark);
        --tl-sys-color-on-surface-variant: var(--tl-sys-color-on-surface-variant-dark);
        --tl-sys-color-inverse-surface: var(--tl-sys-color-inverse-surface-dark);
        --tl-sys-color-inverse-on-surface: var(--tl-sys-color-inverse-on-surface-dark);
      }
    }

    @media (prefers-color-scheme: dark) {
        --tl-sys-color-primary: var(--tl-sys-color-primary-dark);
        --tl-sys-color-on-primary: var(--tl-sys-color-on-primary-dark);
        --tl-sys-color-primary-container: var(--tl-sys-color-primary-container-dark);
        --tl-sys-color-on-primary-container: var(--tl-sys-color-on-primary-container-dark);
        --tl-sys-color-secondary: var(--tl-sys-color-secondary-dark);
        --tl-sys-color-on-secondary: var(--tl-sys-color-on-secondary-dark);
        --tl-sys-color-secondary-container: var(--tl-sys-color-secondary-container-dark);
        --tl-sys-color-on-secondary-container: var(--tl-sys-color-on-secondary-container-dark);
        --tl-sys-color-tertiary: var(--tl-sys-color-tertiary-dark);
        --tl-sys-color-on-tertiary: var(--tl-sys-color-on-tertiary-dark);
        --tl-sys-color-tertiary-container: var(--tl-sys-color-tertiary-container-dark);
        --tl-sys-color-on-tertiary-container: var(--tl-sys-color-on-tertiary-container-dark);
        --tl-sys-color-error: var(--tl-sys-color-error-dark);
        --tl-sys-color-on-error: var(--tl-sys-color-on-error-dark);
        --tl-sys-color-error-container: var(--tl-sys-color-error-container-dark);
        --tl-sys-color-on-error-container: var(--tl-sys-color-on-error-container-dark);
        --tl-sys-color-outline: var(--tl-sys-color-outline-dark);
        --tl-sys-color-background: var(--tl-sys-color-background-dark);
        --tl-sys-color-on-background: var(--tl-sys-color-on-background-dark);
        --tl-sys-color-surface: var(--tl-sys-color-surface-dark);
        --tl-sys-color-on-surface: var(--tl-sys-color-on-surface-dark);
        --tl-sys-color-surface-variant: var(--tl-sys-color-surface-variant-dark);
        --tl-sys-color-on-surface-variant: var(--tl-sys-color-on-surface-variant-dark);
        --tl-sys-color-inverse-surface: var(--tl-sys-color-inverse-surface-dark);
        --tl-sys-color-inverse-on-surface: var(--tl-sys-color-inverse-on-surface-dark);
      
      .light-theme {
        --tl-sys-color-primary: var(--tl-sys-color-primary-light);
        --tl-sys-color-on-primary: var(--tl-sys-color-on-primary-light);
        --tl-sys-color-primary-container: var(--tl-sys-color-primary-container-light);
        --tl-sys-color-on-primary-container: var(--tl-sys-color-on-primary-container-light);
        --tl-sys-color-secondary: var(--tl-sys-color-secondary-light);
        --tl-sys-color-on-secondary: var(--tl-sys-color-on-secondary-light);
        --tl-sys-color-secondary-container: var(--tl-sys-color-secondary-container-light);
        --tl-sys-color-on-secondary-container: var(--tl-sys-color-on-secondary-container-light);
        --tl-sys-color-tertiary: var(--tl-sys-color-tertiary-light);
        --tl-sys-color-on-tertiary: var(--tl-sys-color-on-tertiary-light);
        --tl-sys-color-tertiary-container: var(--tl-sys-color-tertiary-container-light);
        --tl-sys-color-on-tertiary-container: var(--tl-sys-color-on-tertiary-container-light);
        --tl-sys-color-error: var(--tl-sys-color-error-light);
        --tl-sys-color-on-error: var(--tl-sys-color-on-error-light);
        --tl-sys-color-error-container: var(--tl-sys-color-error-container-light);
        --tl-sys-color-on-error-container: var(--tl-sys-color-on-error-container-light);
        --tl-sys-color-outline: var(--tl-sys-color-outline-light);
        --tl-sys-color-background: var(--tl-sys-color-background-light);
        --tl-sys-color-on-background: var(--tl-sys-color-on-background-light);
        --tl-sys-color-surface: var(--tl-sys-color-surface-light);
        --tl-sys-color-on-surface: var(--tl-sys-color-on-surface-light);
        --tl-sys-color-surface-variant: var(--tl-sys-color-surface-variant-light);
        --tl-sys-color-on-surface-variant: var(--tl-sys-color-on-surface-variant-light);
        --tl-sys-color-inverse-surface: var(--tl-sys-color-inverse-surface-light);
        --tl-sys-color-inverse-on-surface: var(--tl-sys-color-inverse-on-surface-light);
      }
  }
`;

interface Accent {
  base: string,
  on: string,
  container: string,
  onContainer: string,
}

export interface ColorSystem {
  primary: Accent,
  secondary: Accent,
  tertiary: Accent,
  error: Accent,
  outline: Pick<Accent, "base">,
  background: Pick<Accent, "base" | "on">,
  surface: Pick<Accent, "base" | "on"> & {
    variant: Pick<Accent, "base" | "on">,
    inverse: Pick<Accent, "base" | "on">
  }
}