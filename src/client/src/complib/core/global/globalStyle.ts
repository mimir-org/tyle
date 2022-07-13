import { createGlobalStyle } from "styled-components/macro";
import { variablesColor } from "../variables/color/variablesColor";
import { variablesTypography } from "../variables/typography/variablesTypography";
import { variablesBorder } from "../variables/variablesBorder";
import { variablesElevation } from "../variables/variablesElevation";
import { variablesQueries } from "../variables/variablesQueries";
import { variablesShadow } from "../variables/variablesShadow";
import { variablesSpacing } from "../variables/variablesSpacing";
import { variablesState } from "../variables/variablesState";
import { globalResetStyle } from "./globalResetStyle";
import { globalTypographyStyle } from "./globalTypographyStyle";

export const GlobalStyle = createGlobalStyle`
  // CSS RESET
  ${globalResetStyle}
  
  // APPLICATION SPECIFIC
  ${({ theme }) => globalTypographyStyle(theme.tyle)}
  
  // CSS VARIABLES
  ${variablesColor}
  ${variablesSpacing}
  ${variablesShadow}
  ${variablesBorder}
  ${variablesTypography}
  ${variablesElevation}
  ${variablesState}
  ${variablesQueries}
`;
