import { animation, AnimationSystem } from "./animation";
import { border, BorderSystem } from "./border";
import { ColorSystem, dark, light } from "./color";
import { elevation, ElevationSystem } from "./elevation";
import { query, QuerySystem } from "./query";
import { shadow, ShadowSystem } from "./shadow";
import { spacing, SpacingSystem } from "./spacing";
import { state, StateSystem } from "./state";
import { typography, TypographySystem } from "./typography";

export interface Theme {
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

export const theme: Theme = {
  border: border,
  color: light,
  typography: typography,
  shadow: shadow,
  spacing: spacing,
  state: state,
  elevation: elevation,
  animation: animation,
  queries: query,
};

export const themeBuilder = (colorTheme: string): Theme => {
  let targetTheme = light;

  if (colorTheme === "dark") {
    targetTheme = dark;
  }

  return {
    ...theme,
    color: {
      ...theme.color,
      ...targetTheme,
    },
  };
};
