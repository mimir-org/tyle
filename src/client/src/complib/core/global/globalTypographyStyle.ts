import { css } from "styled-components/macro";
import { getTextRole } from "../../mixins";
import { TyleTheme } from "../theme/theme";

/**
 * TYPOGRAPHY SPECIFIC GLOBALS
 */
export const globalTypographyStyle = (theme: TyleTheme) => css`
  body {
    font-family: ${theme.typography.ref.typeface.brand};
    font-weight: ${theme.typography.ref.typeface.weights.normal};
    font-size: 100%;
    color: ${theme.color.sys.background.on};
  }

  h1 {
    ${getTextRole("display-large")}
  }

  h2 {
    ${getTextRole("display-medium")}
  }

  h3 {
    ${getTextRole("display-small")}
  }

  h4 {
    ${getTextRole("headline-large")}
  }

  h5 {
    ${getTextRole("headline-medium")}
  }

  h6 {
    ${getTextRole("headline-small")}
  }

  p,
  a {
    ${getTextRole("body-large")}
  }

  a:hover {
    text-decoration: underline;
    cursor: pointer;
  }

  b,
  strong {
    ${getTextRole("body-large")}
    font-weight: ${theme.typography.ref.typeface.weights.bold};
  }

  small {
    ${getTextRole("body-small")}
  }
`;
