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
