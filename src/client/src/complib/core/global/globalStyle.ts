import { globalResetStyle } from "complib/core/global/globalResetStyle";
import { globalTypographyStyle } from "complib/core/global/globalTypographyStyle";
import { variablesColor } from "complib/core/variables/color/variablesColor";
import { variablesTypography } from "complib/core/variables/typography/variablesTypography";
import { variablesBorder } from "complib/core/variables/variablesBorder";
import { variablesElevation } from "complib/core/variables/variablesElevation";
import { variablesQueries } from "complib/core/variables/variablesQueries";
import { variablesShadow } from "complib/core/variables/variablesShadow";
import { variablesSpacing } from "complib/core/variables/variablesSpacing";
import { variablesState } from "complib/core/variables/variablesState";
import { createGlobalStyle } from "styled-components/macro";

export const GlobalStyle = createGlobalStyle`
  ${globalResetStyle}
  ${({ theme }) => globalTypographyStyle(theme.mimirorg)}
  body {
    background-color: ${(props) => props.theme.mimirorg.color.background.base}
  }

  ${variablesColor}
  ${variablesSpacing}
  ${variablesShadow}
  ${variablesBorder}
  ${variablesTypography}
  ${variablesElevation}
  ${variablesState}
  ${variablesQueries}
`;
