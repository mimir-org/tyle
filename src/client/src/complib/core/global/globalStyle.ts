import { createGlobalStyle } from "styled-components/macro";
import { globalResetStyle } from "./globalResetStyle";
import { globalTypographyStyle } from "./globalTypographyStyle";
import { variablesSpacing } from "../variables/variablesSpacing";
import { variablesTypography } from "../variables/typography/variablesTypography";
import { variablesShadow } from "../variables/variablesShadow";
import { variablesBorder } from "../variables/variablesBorder";
import { variablesColor } from "../variables/variablesColor";
import { variablesElevation } from "../variables/variablesElevation";
import { variablesState } from "../variables/variablesState";

export const GlobalStyle = createGlobalStyle`
  // CSS RESET
  ${globalResetStyle}
  
  // APPLICATION SPECIFIC
  ${({ theme }) => globalTypographyStyle(theme.typeLibrary)}
  
  // CSS VARIABLES
  ${variablesColor}
  ${variablesSpacing}
  ${variablesShadow}
  ${variablesBorder}
  ${variablesTypography}
  ${variablesElevation}
  ${variablesState}
`;
