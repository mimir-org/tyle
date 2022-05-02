import { spacing, SpacingSystem } from "../variables/variablesSpacing";
import { border, BorderSystem } from "../variables/variablesBorder";
import { typography, TypographySystem } from "../variables/typography/variablesTypography";
import { shadow, ShadowSystem } from "../variables/variablesShadow";
import { state, StateSystem } from "../variables/variablesState";
import { elevation, ElevationSystem } from "../variables/variablesElevation";
import { color, ColorSystem, darkTheme, lightTheme } from "../variables/variablesColor";
import { animation, AnimationSystem } from "../variables/variablesAnimation";

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
  state: state,
  elevation: elevation,
  animation: animation,
};

export interface TypeLibraryTheme {
  border: BorderSystem;
  color: ColorSystem;
  typography: TypographySystem;
  shadow: ShadowSystem;
  spacing: SpacingSystem;
  state: StateSystem;
  elevation: ElevationSystem;
  animation: AnimationSystem;
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
