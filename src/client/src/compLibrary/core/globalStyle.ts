import { createGlobalStyle } from "styled-components/macro";
import { variablesColor } from "./variables/variablesColor";
import { variablesUtil } from "./variables/variablesUtil";
import { variablesFont } from "./variables/variablesFont";
import { globalResetStyle } from "./global/globalResetStyle";
import { globalApplicationStyle } from "./global/globalApplicationStyle";

export const GlobalStyle = createGlobalStyle`
  // CSS VARIABLES
  ${variablesColor}
  ${variablesUtil}
  ${variablesFont}
  
  // CSS RESET
  ${globalResetStyle}
  
  // APPLICATION SPECIFIC
  ${globalApplicationStyle}
`;
