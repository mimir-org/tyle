import { darkTheme } from "complib/core/variables/color/themes/darkTheme";
import { lightTheme } from "complib/core/variables/color/themes/lightTheme";
import { ColorSystem } from "complib/core/variables/color/types/colorSystem";
import { color } from "complib/core/variables/color/variablesColor";
import { typography, TypographySystem } from "complib/core/variables/typography/variablesTypography";
import { animation, AnimationSystem } from "complib/core/variables/variablesAnimation";
import { border, BorderSystem } from "complib/core/variables/variablesBorder";
import { elevation, ElevationSystem } from "complib/core/variables/variablesElevation";
import { queries, QuerySystem } from "complib/core/variables/variablesQueries";
import { shadow, ShadowSystem } from "complib/core/variables/variablesShadow";
import { spacing, SpacingSystem } from "complib/core/variables/variablesSpacing";
import { state, StateSystem } from "complib/core/variables/variablesState";

/**
 * Exposes all theme variables through a typed object.
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
  queries: queries,
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
  queries: QuerySystem;
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
