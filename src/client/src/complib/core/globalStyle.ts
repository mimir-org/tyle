import { createGlobalStyle } from "styled-components/macro";
import { globalResetStyle } from "./global/globalResetStyle";
import { globalTypographyStyle } from "./global/globalTypographyStyle";
import { variablesColor } from "./variables/variablesColor";
import { variablesSpacing } from "./variables/variablesSpacing";
import { variablesFont } from "./variables/variablesFont";
import { variablesShadow } from "./variables/variablesShadow";
import { variablesBorder } from "./variables/variablesBorder";

export const GlobalStyle = createGlobalStyle`
  // CSS VARIABLES
  ${variablesColor}
  ${variablesSpacing}
  ${variablesShadow}
  ${variablesBorder}
  ${variablesFont}
  
  // CSS RESET
  ${globalResetStyle}
  
  // APPLICATION SPECIFIC
  ${globalTypographyStyle}
`;
