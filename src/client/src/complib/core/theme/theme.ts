import { darkTheme } from "../variables/color/themes/darkTheme";
import { lightTheme } from "../variables/color/themes/lightTheme";
import { ColorSystem } from "../variables/color/types/colorSystem";
import { color } from "../variables/color/variablesColor";
import { typography, TypographySystem } from "../variables/typography/variablesTypography";
import { animation, AnimationSystem } from "../variables/variablesAnimation";
import { border, BorderSystem } from "../variables/variablesBorder";
import { elevation, ElevationSystem } from "../variables/variablesElevation";
import { shadow, ShadowSystem } from "../variables/variablesShadow";
import { spacing, SpacingSystem } from "../variables/variablesSpacing";
import { state, StateSystem } from "../variables/variablesState";

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

export interface TyleTheme {
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
    tyle: TyleTheme;
  }
}

export const themeBuilder = (colorTheme: string): TyleTheme => {
  return {
    ...theme,
    color: {
      ...theme.color,
      sys: colorTheme === "dark" ? darkTheme : lightTheme,
    },
  };
};
