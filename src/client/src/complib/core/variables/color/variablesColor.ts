import { css } from "styled-components/macro";
import { colorReference } from "./reference/colorReference";
import { darkTheme } from "./themes/darkTheme";
import { lightTheme } from "./themes/lightTheme";
import { ColorSystem } from "./types/colorSystem";

export const color: ColorSystem = {
  ref: colorReference,
  sys: lightTheme
};

export const variablesColor = css`
  :root {
    // Reference palette    
    --tl-ref-color-primary-0: ${colorReference.primary["0"]};
    --tl-ref-color-primary-10: ${colorReference.primary["10"]};
    --tl-ref-color-primary-20: ${colorReference.primary["20"]};
    --tl-ref-color-primary-30: ${colorReference.primary["30"]};
    --tl-ref-color-primary-40: ${colorReference.primary["40"]};
    --tl-ref-color-primary-50: ${colorReference.primary["50"]};
    --tl-ref-color-primary-60: ${colorReference.primary["60"]};
    --tl-ref-color-primary-70: ${colorReference.primary["70"]};
    --tl-ref-color-primary-80: ${colorReference.primary["80"]};
    --tl-ref-color-primary-90: ${colorReference.primary["90"]};
    --tl-ref-color-primary-95: ${colorReference.primary["95"]};
    --tl-ref-color-primary-99: ${colorReference.primary["99"]};
    --tl-ref-color-primary-100: ${colorReference.primary["100"]};
    
    --tl-ref-color-secondary-0: ${colorReference.secondary["0"]};
    --tl-ref-color-secondary-10: ${colorReference.secondary["10"]};
    --tl-ref-color-secondary-20: ${colorReference.secondary["20"]};
    --tl-ref-color-secondary-30: ${colorReference.secondary["30"]};
    --tl-ref-color-secondary-40: ${colorReference.secondary["40"]};
    --tl-ref-color-secondary-50: ${colorReference.secondary["50"]};
    --tl-ref-color-secondary-60: ${colorReference.secondary["60"]};
    --tl-ref-color-secondary-70: ${colorReference.secondary["70"]};
    --tl-ref-color-secondary-80: ${colorReference.secondary["80"]};
    --tl-ref-color-secondary-90: ${colorReference.secondary["90"]};
    --tl-ref-color-secondary-95: ${colorReference.secondary["95"]};
    --tl-ref-color-secondary-99: ${colorReference.secondary["99"]};
    --tl-ref-color-secondary-100: ${colorReference.secondary["100"]};
    
    --tl-ref-color-tertiary-0: ${colorReference.tertiary["0"]};         
    --tl-ref-color-tertiary-10: ${colorReference.tertiary["10"]};       
    --tl-ref-color-tertiary-20: ${colorReference.tertiary["20"]};       
    --tl-ref-color-tertiary-30: ${colorReference.tertiary["30"]};       
    --tl-ref-color-tertiary-40: ${colorReference.tertiary["40"]};       
    --tl-ref-color-tertiary-50: ${colorReference.tertiary["50"]};       
    --tl-ref-color-tertiary-60: ${colorReference.tertiary["60"]};       
    --tl-ref-color-tertiary-70: ${colorReference.tertiary["70"]};       
    --tl-ref-color-tertiary-80: ${colorReference.tertiary["80"]};       
    --tl-ref-color-tertiary-90: ${colorReference.tertiary["90"]};       
    --tl-ref-color-tertiary-95: ${colorReference.tertiary["95"]};       
    --tl-ref-color-tertiary-99: ${colorReference.tertiary["99"]};       
    --tl-ref-color-tertiary-100: ${colorReference.tertiary["100"]};     
    
    --tl-ref-color-error-0: ${colorReference.error["0"]};         
    --tl-ref-color-error-10: ${colorReference.error["10"]};       
    --tl-ref-color-error-20: ${colorReference.error["20"]};       
    --tl-ref-color-error-30: ${colorReference.error["30"]};       
    --tl-ref-color-error-40: ${colorReference.error["40"]};       
    --tl-ref-color-error-50: ${colorReference.error["50"]};       
    --tl-ref-color-error-60: ${colorReference.error["60"]};       
    --tl-ref-color-error-70: ${colorReference.error["70"]};       
    --tl-ref-color-error-80: ${colorReference.error["80"]};       
    --tl-ref-color-error-90: ${colorReference.error["90"]};       
    --tl-ref-color-error-95: ${colorReference.error["95"]};       
    --tl-ref-color-error-99: ${colorReference.error["99"]};       
    --tl-ref-color-error-100: ${colorReference.error["100"]};     
    
    --tl-ref-color-neutral-0: ${colorReference.neutral["0"]};         
    --tl-ref-color-neutral-10: ${colorReference.neutral["10"]};       
    --tl-ref-color-neutral-20: ${colorReference.neutral["20"]};       
    --tl-ref-color-neutral-30: ${colorReference.neutral["30"]};       
    --tl-ref-color-neutral-40: ${colorReference.neutral["40"]};       
    --tl-ref-color-neutral-50: ${colorReference.neutral["50"]};       
    --tl-ref-color-neutral-60: ${colorReference.neutral["60"]};       
    --tl-ref-color-neutral-70: ${colorReference.neutral["70"]};       
    --tl-ref-color-neutral-80: ${colorReference.neutral["80"]};       
    --tl-ref-color-neutral-90: ${colorReference.neutral["90"]};       
    --tl-ref-color-neutral-95: ${colorReference.neutral["95"]};       
    --tl-ref-color-neutral-99: ${colorReference.neutral["99"]};       
    --tl-ref-color-neutral-100: ${colorReference.neutral["100"]};     
    
    --tl-ref-color-neutralVariant-0: ${colorReference.neutralVariant["0"]};         
    --tl-ref-color-neutralVariant-10: ${colorReference.neutralVariant["10"]};       
    --tl-ref-color-neutralVariant-20: ${colorReference.neutralVariant["20"]};       
    --tl-ref-color-neutralVariant-30: ${colorReference.neutralVariant["30"]};       
    --tl-ref-color-neutralVariant-40: ${colorReference.neutralVariant["40"]};       
    --tl-ref-color-neutralVariant-50: ${colorReference.neutralVariant["50"]};       
    --tl-ref-color-neutralVariant-60: ${colorReference.neutralVariant["60"]};       
    --tl-ref-color-neutralVariant-70: ${colorReference.neutralVariant["70"]};       
    --tl-ref-color-neutralVariant-80: ${colorReference.neutralVariant["80"]};       
    --tl-ref-color-neutralVariant-90: ${colorReference.neutralVariant["90"]};       
    --tl-ref-color-neutralVariant-95: ${colorReference.neutralVariant["95"]};       
    --tl-ref-color-neutralVariant-99: ${colorReference.neutralVariant["99"]};       
    --tl-ref-color-neutralVariant-100: ${colorReference.neutralVariant["100"]};     
    
    // Light theme  
    --tl-sys-color-primary-light: ${lightTheme.primary.base};
    --tl-sys-color-on-primary-light: ${lightTheme.primary.on};
    --tl-sys-color-secondary-light: ${lightTheme.secondary.base};
    --tl-sys-color-on-secondary-light: ${lightTheme.secondary.on};
    --tl-sys-color-tertiary-light: ${lightTheme.tertiary.base};
    --tl-sys-color-on-tertiary-light: ${lightTheme.tertiary.on};
    --tl-sys-color-tertiary-container-light: ${lightTheme.tertiary.container?.base};
    --tl-sys-color-on-tertiary-container-light: ${lightTheme.tertiary.container?.on};
    --tl-sys-color-error-light: ${lightTheme.error.base};
    --tl-sys-color-on-error-light: ${lightTheme.error.on};
    --tl-sys-color-outline-light: ${lightTheme.outline.base};
    --tl-sys-color-background-light: ${lightTheme.background.base};
    --tl-sys-color-on-background-light: ${lightTheme.background.on};
    --tl-sys-color-surface-light: ${lightTheme.surface.base};
    --tl-sys-color-surface-tint-light: ${lightTheme.surface.tint.base};
    --tl-sys-color-on-surface-light: ${lightTheme.surface.on};
    --tl-sys-color-surface-variant-light: ${lightTheme.surface.variant.base};
    --tl-sys-color-on-surface-variant-light: ${lightTheme.surface.variant.on};
    --tl-sys-color-inverse-surface-light: ${lightTheme.surface.inverse.base};
    --tl-sys-color-inverse-on-surface-light: ${lightTheme.surface.inverse.on};
    --tl-sys-color-shadow-light: ${lightTheme.shadow.base};

    // Dark theme
    --tl-sys-color-primary-dark: ${darkTheme.primary.base};
    --tl-sys-color-on-primary-dark: ${darkTheme.primary.on};
    --tl-sys-color-secondary-dark: ${darkTheme.secondary.base};
    --tl-sys-color-on-secondary-dark: ${darkTheme.secondary.on};
    --tl-sys-color-tertiary-dark: ${darkTheme.tertiary.base};
    --tl-sys-color-on-tertiary-dark: ${darkTheme.tertiary.on};
    --tl-sys-color-tertiary-container-dark: ${darkTheme.tertiary.container?.base};
    --tl-sys-color-on-tertiary-container-dark: ${darkTheme.tertiary.container?.on};
    --tl-sys-color-error-dark: ${darkTheme.error.base};
    --tl-sys-color-on-error-dark: ${darkTheme.error.on};
    --tl-sys-color-outline-dark: ${darkTheme.outline.base};
    --tl-sys-color-background-dark: ${darkTheme.background.base};
    --tl-sys-color-on-background-dark: ${darkTheme.background.on};
    --tl-sys-color-surface-dark: ${darkTheme.surface.base};
    --tl-sys-color-surface-tint-dark: ${darkTheme.surface.tint.base};
    --tl-sys-color-on-surface-dark: ${darkTheme.surface.on};
    --tl-sys-color-surface-variant-dark: ${darkTheme.surface.variant.base};
    --tl-sys-color-on-surface-variant-dark: ${darkTheme.surface.variant.on};
    --tl-sys-color-inverse-surface-dark: ${darkTheme.surface.inverse.base};
    --tl-sys-color-inverse-on-surface-dark: ${darkTheme.surface.inverse.on};
    --tl-sys-color-shadow-dark: ${darkTheme.shadow.base};

    @media (prefers-color-scheme: light) {
        --tl-sys-color-primary: var(--tl-sys-color-primary-light);
        --tl-sys-color-on-primary: var(--tl-sys-color-on-primary-light);
        --tl-sys-color-secondary: var(--tl-sys-color-secondary-light);
        --tl-sys-color-on-secondary: var(--tl-sys-color-on-secondary-light);
        --tl-sys-color-tertiary: var(--tl-sys-color-tertiary-light);
        --tl-sys-color-on-tertiary: var(--tl-sys-color-on-tertiary-light);
        --tl-sys-color-tertiary-container: var(--tl-sys-color-tertiary-container-light);
        --tl-sys-color-on-tertiary-container: var(--tl-sys-color-on-tertiary-container-light);
        --tl-sys-color-error: var(--tl-sys-color-error-light);
        --tl-sys-color-on-error: var(--tl-sys-color-on-error-light);
        --tl-sys-color-outline: var(--tl-sys-color-outline-light);
        --tl-sys-color-background: var(--tl-sys-color-background-light);
        --tl-sys-color-on-background: var(--tl-sys-color-on-background-light);
        --tl-sys-color-surface: var(--tl-sys-color-surface-light);
        --tl-sys-color-surface-tint: var(--tl-sys-color-surface-tint-light);
        --tl-sys-color-on-surface: var(--tl-sys-color-on-surface-light);
        --tl-sys-color-surface-variant: var(--tl-sys-color-surface-variant-light);
        --tl-sys-color-on-surface-variant: var(--tl-sys-color-on-surface-variant-light);
        --tl-sys-color-inverse-surface: var(--tl-sys-color-inverse-surface-light);
        --tl-sys-color-inverse-on-surface: var(--tl-sys-color-inverse-on-surface-light);
        --tl-sys-color-shadow: var(--tl-sys-color-shadow-light);
      
      .dark-theme {
        --tl-sys-color-primary: var(--tl-sys-color-primary-dark);
        --tl-sys-color-on-primary: var(--tl-sys-color-on-primary-dark);
        --tl-sys-color-secondary: var(--tl-sys-color-secondary-dark);
        --tl-sys-color-on-secondary: var(--tl-sys-color-on-secondary-dark);
        --tl-sys-color-tertiary: var(--tl-sys-color-tertiary-dark);
        --tl-sys-color-on-tertiary: var(--tl-sys-color-on-tertiary-dark);
        --tl-sys-color-tertiary-container: var(--tl-sys-color-tertiary-container-dark);
        --tl-sys-color-on-tertiary-container: var(--tl-sys-color-on-tertiary-container-dark);
        --tl-sys-color-error: var(--tl-sys-color-error-dark);
        --tl-sys-color-on-error: var(--tl-sys-color-on-error-dark);
        --tl-sys-color-outline: var(--tl-sys-color-outline-dark);
        --tl-sys-color-background: var(--tl-sys-color-background-dark);
        --tl-sys-color-on-background: var(--tl-sys-color-on-background-dark);
        --tl-sys-color-surface: var(--tl-sys-color-surface-dark);
        --tl-sys-color-surface-tint: var(--tl-sys-color-surface-tint-dark);
        --tl-sys-color-on-surface: var(--tl-sys-color-on-surface-dark);
        --tl-sys-color-surface-variant: var(--tl-sys-color-surface-variant-dark);
        --tl-sys-color-on-surface-variant: var(--tl-sys-color-on-surface-variant-dark);
        --tl-sys-color-inverse-surface: var(--tl-sys-color-inverse-surface-dark);
        --tl-sys-color-inverse-on-surface: var(--tl-sys-color-inverse-on-surface-dark);
        --tl-sys-color-shadow: var(--tl-sys-color-shadow-dark);
      }
    }

    @media (prefers-color-scheme: dark) {
        --tl-sys-color-primary: var(--tl-sys-color-primary-dark);
        --tl-sys-color-on-primary: var(--tl-sys-color-on-primary-dark);
        --tl-sys-color-secondary: var(--tl-sys-color-secondary-dark);
        --tl-sys-color-on-secondary: var(--tl-sys-color-on-secondary-dark);
        --tl-sys-color-tertiary: var(--tl-sys-color-tertiary-dark);
        --tl-sys-color-on-tertiary: var(--tl-sys-color-on-tertiary-dark);
        --tl-sys-color-tertiary-container: var(--tl-sys-color-tertiary-container-dark);
        --tl-sys-color-on-tertiary-container: var(--tl-sys-color-on-tertiary-container-dark);
        --tl-sys-color-error: var(--tl-sys-color-error-dark);
        --tl-sys-color-on-error: var(--tl-sys-color-on-error-dark);
        --tl-sys-color-outline: var(--tl-sys-color-outline-dark);
        --tl-sys-color-background: var(--tl-sys-color-background-dark);
        --tl-sys-color-on-background: var(--tl-sys-color-on-background-dark);
        --tl-sys-color-surface: var(--tl-sys-color-surface-dark);
        --tl-sys-color-surface-tint: var(--tl-sys-color-surface-tint-dark);
        --tl-sys-color-on-surface: var(--tl-sys-color-on-surface-dark);
        --tl-sys-color-surface-variant: var(--tl-sys-color-surface-variant-dark);
        --tl-sys-color-on-surface-variant: var(--tl-sys-color-on-surface-variant-dark);
        --tl-sys-color-inverse-surface: var(--tl-sys-color-inverse-surface-dark);
        --tl-sys-color-inverse-on-surface: var(--tl-sys-color-inverse-on-surface-dark);
        --tl-sys-color-shadow: var(--tl-sys-color-shadow-dark);
      
      .light-theme {
        --tl-sys-color-primary: var(--tl-sys-color-primary-light);
        --tl-sys-color-on-primary: var(--tl-sys-color-on-primary-light);
        --tl-sys-color-secondary: var(--tl-sys-color-secondary-light);
        --tl-sys-color-on-secondary: var(--tl-sys-color-on-secondary-light);
        --tl-sys-color-tertiary: var(--tl-sys-color-tertiary-light);
        --tl-sys-color-on-tertiary: var(--tl-sys-color-on-tertiary-light);
        --tl-sys-color-tertiary-container: var(--tl-sys-color-tertiary-container-light);
        --tl-sys-color-on-tertiary-container: var(--tl-sys-color-on-tertiary-container-light);
        --tl-sys-color-error: var(--tl-sys-color-error-light);
        --tl-sys-color-on-error: var(--tl-sys-color-on-error-light);
        --tl-sys-color-outline: var(--tl-sys-color-outline-light);
        --tl-sys-color-background: var(--tl-sys-color-background-light);
        --tl-sys-color-on-background: var(--tl-sys-color-on-background-light);
        --tl-sys-color-surface: var(--tl-sys-color-surface-light);
        --tl-sys-color-surface-tint: var(--tl-sys-color-surface-tint-light);
        --tl-sys-color-on-surface: var(--tl-sys-color-on-surface-light);
        --tl-sys-color-surface-variant: var(--tl-sys-color-surface-variant-light);
        --tl-sys-color-on-surface-variant: var(--tl-sys-color-on-surface-variant-light);
        --tl-sys-color-inverse-surface: var(--tl-sys-color-inverse-surface-light);
        --tl-sys-color-inverse-on-surface: var(--tl-sys-color-inverse-on-surface-light);
        --tl-sys-color-shadow: var(--tl-sys-color-shadow-light);
      }
  }
`;