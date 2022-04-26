import { spacing } from "../variables/variablesSpacing";
import { border } from "../variables/variablesBorder";
import { typography } from "../variables/variablesTypography";
import { shadow } from "../variables/variablesShadow";
import { color } from "../variables/variablesColor";

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
