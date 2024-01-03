import { css } from "styled-components";
import { Display, Ellipsis, Flex, Palette, Sizing, Spacings, Typography } from "types/styleProps";

export const displayMixin = css<Display>`
  display: ${(props) => props.display};
  overflow: ${(props) => props.overflow};
  text-overflow: ${(props) => props.textOverflow};
  visibility: ${(props) => props.visibility};
  white-space: ${(props) => props.whiteSpace};
`;

export const ellipsisMixin = css<Ellipsis>`
  ${({ useEllipsis, ellipsisMaxLines }) => {
    if (!useEllipsis) return "";

    if (ellipsisMaxLines === 1)
      return css`
        text-overflow: ellipsis;
        overflow: hidden;
        white-space: nowrap;
      `;

    if (ellipsisMaxLines && ellipsisMaxLines > 1)
      return css`
        display: -webkit-box;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: ${ellipsisMaxLines};
        overflow: hidden;
      `;
  }}
`;

export const flexMixin = css<Flex>`
  flex: ${(props) => props.flex};
  flex-grow: ${(props) => props.flexGrow};
  flex-wrap: ${(props) => props.flexWrap};
  flex-shrink: ${(props) => props.flexShrink};
  flex-direction: ${(props) => props.flexDirection};
  flex-flow: ${(props) => props.flexFlow};
  justify-content: ${(props) => props.justifyContent};
  align-items: ${(props) => props.alignItems};
  align-content: ${(props) => props.alignContent};
  align-self: ${(props) => props.alignSelf};
  order: ${(props) => props.order};
  gap: ${(props) => props.gap};
`;

/**
 * Focus styles without pseudo-class wrapper
 */
export const focusRaw = css`
  outline: 1px solid ${(props) => props.theme.tyle.color.primary.base};
  outline-offset: 1px;
`;

/**
 * Common focus style for several components.
 * Uses :focus-visible as pseudo-class.
 */
export const focus = css`
  :focus-visible {
    ${focusRaw};
  }
`;

export const paletteMixin = css<Palette>`
  color: ${(props) => props.color};
  background-color: ${(props) => props.bgColor};
`;

export const sizingMixin = css<Sizing>`
  width: ${(props) => props.width};
  max-width: ${(props) => props.maxWidth};
  min-width: ${(props) => props.minWidth};
  height: ${(props) => props.height};
  max-height: ${(props) => props.maxHeight};
  min-height: ${(props) => props.minHeight};
  box-sizing: ${(props) => props.boxSizing};
`;

export const spacingMixin = css<Spacings>`
  padding: ${(props) => props.spacing?.p};
  ${(props) =>
    props.spacing?.px &&
    `
    padding-left: ${props.spacing?.px};
    padding-right: ${props.spacing?.px};
  `}
  ${(props) =>
    props.spacing?.py &&
    `
    padding-top: ${props.spacing?.py};
    padding-bottom: ${props.spacing?.py};
  `}
  padding-top: ${(props) => props.spacing?.pt};
  padding-right: ${(props) => props.spacing?.pr};
  padding-bottom: ${(props) => props.spacing?.pb};
  padding-left: ${(props) => props.spacing?.pl};

  margin: ${(props) => props.spacing?.m};
  ${(props) =>
    props.spacing?.mx &&
    `
    margin-left: ${props.spacing?.mx};
    margin-right: ${props.spacing?.mx};
  `}
  ${(props) =>
    props.spacing?.my &&
    `
    margin-top: ${props.spacing?.my};
    margin-bottom: ${props.spacing?.my};
  `}
  margin-top: ${(props) => props.spacing?.mt};
  margin-right: ${(props) => props.spacing?.mr};
  margin-bottom: ${(props) => props.spacing?.mb};
  margin-left: ${(props) => props.spacing?.ml};
`;

export const typographyMixin = css<Typography>`
  font: ${(props) => props.font};
  font-family: ${(props) => props.fontFamily};
  font-size: ${(props) => props.fontSize};
  font-style: ${(props) => props.fontStyle};
  font-weight: ${(props) => props.fontWeight};
  letter-spacing: ${(props) => props.letterSpacing};
  line-height: ${(props) => props.lineHeight};
  text-align: ${(props) => props.textAlign};
  text-transform: ${(props) => props.textTransform};
  word-break: ${(props) => props.wordBreak};
`;
