import { css } from "styled-components/macro";

export const variablesColor = css`
  :root {
    --tl-sys-color-primary-light: #5d6408;
    --tl-sys-color-on-primary-light: #ffffff;
    --tl-sys-color-primary-container-light: #e1eb7b;
    --tl-sys-color-on-primary-container-light: #1a1e01;
    
    --tl-sys-color-secondary-light: #5f6145;
    --tl-sys-color-on-secondary-light: #ffffff;
    --tl-sys-color-secondary-container-light: #e3e5c3;
    --tl-sys-color-on-secondary-container-light: #1b1d08;
    
    --tl-sys-color-tertiary-light: #4c665b;
    --tl-sys-color-on-tertiary-light: #ffffff;
    --tl-sys-color-tertiary-container-light: #ceecde;
    --tl-sys-color-on-tertiary-container-light: #0f2019;
    
    --tl-sys-color-error-light: #9d1714;
    --tl-sys-color-on-error-light: #ffffff;
    --tl-sys-color-error-container-light: #f4dad4;
    --tl-sys-color-on-error-container-light: #350000;
    
    --tl-sys-color-outline-light: #78786a;
    
    --tl-sys-color-background-light: #fefcf4;
    --tl-sys-color-on-background-light: #1c1c17;
    
    --tl-sys-color-surface-light: #fefcf4;
    --tl-sys-color-on-surface-light: #1c1c17;
    
    --tl-sys-color-surface-variant-light: #e4e3d2;
    --tl-sys-color-on-surface-variant-light: #47483b;
    
    --tl-sys-color-inverse-primary-light: #c1cf5e;
    --tl-sys-color-inverse-surface-light: #31312b;
    --tl-sys-color-inverse-on-surface-light: #f4f1e9;

    --tl-sys-color-primary-dark: #c5cf62;
    --tl-sys-color-on-primary-dark: #2f3402;
    --tl-sys-color-primary-container-dark: #454b04;
    --tl-sys-color-on-primary-container-dark: #e1eb7b;
    
    --tl-sys-color-secondary-dark: #c7c9a8;
    --tl-sys-color-on-secondary-dark: #30321b;
    --tl-sys-color-secondary-container-dark: #47492f;
    --tl-sys-color-on-secondary-container-dark: #e3e5c3;
    
    --tl-sys-color-tertiary-dark: #b3d0c3;
    --tl-sys-color-on-tertiary-dark: #1f372d;
    --tl-sys-color-tertiary-container-dark: #354e43;
    --tl-sys-color-on-tertiary-container-dark: #ceecde;
    
    --tl-sys-color-error-dark: #eab3a8;
    --tl-sys-color-on-error-dark: #560000;
    --tl-sys-color-error-container-dark: #7b0000;
    --tl-sys-color-on-error-container-dark: #f4dad4;
    
    --tl-sys-color-outline-dark: #929283;
    
    --tl-sys-color-background-dark: #1c1c17;
    --tl-sys-color-on-background-dark: #e4e2da;
    
    --tl-sys-color-surface-dark: #1c1c17;
    --tl-sys-color-on-surface-dark: #e4e2da;
    
    --tl-sys-color-surface-variant-dark: #47483b;
    --tl-sys-color-on-surface-variant-dark: #c8c7b7;
    
    --tl-sys-color-inverse-primary-dark: #5a6400;
    --tl-sys-color-inverse-surface-dark: #e5e2da;
    --tl-sys-color-inverse-on-surface-dark: #1c1c17;

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
        --tl-sys-color-inverse-primary: var(--tl-sys-color-inverse-primary-light);
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
        --tl-sys-color-inverse-primary: var(--tl-sys-color-inverse-primary-dark);
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
        --tl-sys-color-inverse-primary: var(--tl-sys-color-inverse-primary-dark);
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
        --tl-sys-color-inverse-primary: var(--tl-sys-color-inverse-primary-light);
        --tl-sys-color-inverse-surface: var(--tl-sys-color-inverse-surface-light);
        --tl-sys-color-inverse-on-surface: var(--tl-sys-color-inverse-on-surface-light);
      }
  }
`;

export const color = {
  primary: {
    base: "var(--tl-sys-color-primary)",
    on: "var(--tl-sys-color-on-primary)",
    container: "var(--tl-sys-color-primary-container)",
    onContainer: "var(--tl-sys-color-on-primary-container)",
  },
  secondary: {
    base: "var(--tl-sys-color-secondary)",
    on: "var(--tl-sys-color-on-secondary)",
    container: "var(--tl-sys-color-secondary-container)",
    onContainer: "var(--tl-sys-color-on-secondary-container)",
  },
  tertiary: {
    base: "var(--tl-sys-color-tertiary)",
    on: "var(--tl-sys-color-on-tertiary)",
    container: "var(--tl-sys-color-tertiary-container)",
    onContainer: "var(--tl-sys-color-on-tertiary-container)",
  },
  error: {
    base: "var(--tl-sys-color-error)",
    on: "var(--tl-sys-color-on-error)",
    container: "var(--tl-sys-color-error-container)",
    onContainer: "var(--tl-sys-color-on-error-container)",
  },
  outline: {
    base: "var(--tl-sys-color-outline)",
  },
  background: {
    base: "var(--tl-sys-color-background)",
    on: "var(--tl-sys-color-on-background)",
  },
  surface: {
    base: "var(--tl-sys-color-surface)",
    on: "var(--tl-sys-color-on-surface)",
    variant: {
      base: "var(--tl-sys-color-surface-variant)",
      on: "var(--tl-sys-color-on-surface-variant)",
    },
    inverse: {
      base: "var(--tl-sys-color-inverse-surface)",
      on: "var(--tl-sys-color-inverse-on-surface)",
    },
  },
};
