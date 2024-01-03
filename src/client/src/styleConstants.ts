import { css } from "styled-components";
import { Flex } from "types/styleProps";

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
