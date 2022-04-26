import { createGlobalStyle } from "styled-components/macro";
import { globalResetStyle } from "./global/globalResetStyle";
import { globalTypographyStyle } from "./global/globalTypographyStyle";
import { variablesSpacing } from "./variables/variablesSpacing";
import { variablesTypography } from "./variables/variablesTypography";
import { variablesShadow } from "./variables/variablesShadow";
import { variablesBorder } from "./variables/variablesBorder";
import { variablesColor } from "./variables/variablesColor";

export const GlobalStyle = createGlobalStyle`
  // CSS RESET
  ${globalResetStyle}
  
  // APPLICATION SPECIFIC
  ${globalTypographyStyle}
  
  // CSS VARIABLES
  ${variablesColor}
  ${variablesSpacing}
  ${variablesShadow}
  ${variablesBorder}
  ${variablesTypography}
`;
