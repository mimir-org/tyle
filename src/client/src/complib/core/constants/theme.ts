import { SPACING } from "../variables/variablesSpacing";
import { BORDER } from "../variables/variablesBorder";
import { FONT } from "../variables/variablesFont";
import { SHADOW } from "../variables/variablesShadow";
import { COLOR } from "../variables/variablesColor";

/**
 * Exposes all CSS variables through a typed object.
 * Object is meant to be used where presentational components are consumed through container components, and where the
 * consumer might pass a parameter to control design token related values. (e.g borders, colors, spacing etc.)
 */
export const THEME = {
  BORDER,
  COLOR,
  FONT,
  SHADOW,
  SPACING,
};
