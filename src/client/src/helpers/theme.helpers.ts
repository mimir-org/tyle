import { transparentize } from "polished";
import { css } from "styled-components";
import { TextTypes } from "types/styleProps";
import { theme } from "../components/TyleThemeProvider/theme";
import { NominalScale, TypographyRoles } from "../components/TyleThemeProvider/typography";

export const getTextRole = (variant?: TextTypes) => {
  if (!variant) return "";

  const [type, size] = variant.split("-");
  const textType = theme.typography.roles[type as keyof TypographyRoles][size as keyof NominalScale];

  return css`
    font: ${textType.font};
    letter-spacing: ${textType.letterSpacing};
    line-height: ${textType.lineHeight};
  `;
};

export const translucify = (color: string, opacity: number) => {
  return transparentize(1 - opacity, color);
};

/**
 * Hides the scrollbar
 *
 * When using this mixin you should make sure that the UI offers enough scroll affordance,
 * so that the end user is able to discover that the content is scrollable.
 */
export const hideScrollbar = css`
  scrollbar-width: none;
  -ms-overflow-style: none;
  ::-webkit-scrollbar {
    width: 0;
    height: 0;
  }
`;
