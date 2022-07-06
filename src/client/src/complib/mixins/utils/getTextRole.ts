import { css } from "styled-components/macro";
import { NominalScale, theme, TypographyRoles } from "../../core";
import { TextTypes } from "../../props";

export const getTextRole = (variant?: TextTypes) => {
  if (!variant) return "";

  const [type, size] = variant.split("-");
  const textType = theme.typography.sys.roles[type as keyof TypographyRoles][size as keyof NominalScale];

  return css`
    font: ${textType.font};
    letter-spacing: ${textType.letterSpacing};
    line-height: ${textType.lineHeight};
  `;
};
