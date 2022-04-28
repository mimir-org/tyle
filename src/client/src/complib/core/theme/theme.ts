import { spacing, SpacingSystem } from "../variables/variablesSpacing";
import { border, BorderSystem } from "../variables/variablesBorder";
import { typography, TypographySystem } from "../variables/variablesTypography";
import { shadow, ShadowSystem } from "../variables/variablesShadow";
import { color, ColorSystem, darkTheme, lightTheme } from "../variables/variablesColor";

/**
 * Exposes all CSS variables through a typed object.
 * Object is meant to be used where presentational components are consumed through container components, and where the
 * consumer might pass a parameter to control design token related values. (e.g borders, colors, spacing etc.)
 */
export const theme = {
  border: border,
  color: color,
  typography: typography,
  shadow: shadow,
  spacing: spacing,
};

export interface TypeLibraryTheme {
  border: BorderSystem;
  color: ColorSystem;
  typography: TypographySystem;
  shadow: ShadowSystem;
  spacing: SpacingSystem;
}

/**
 * Defines the theme available through styled-component's theme-provider
 */
declare module "styled-components" {
  export interface DefaultTheme {
    typeLibrary: TypeLibraryTheme;
  }
}

export const themeBuilder = (colorTheme: string) => {
  return {
    ...theme,
    color: colorTheme === "dark" ? darkTheme : lightTheme,
  };
};
