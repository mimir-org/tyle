import { css } from "styled-components/macro";
import { Flex } from "../props";

export const flexMixin = css<Flex>`
  flex-direction: ${(props) => props.flexDirection};
  flex-wrap: ${(props) => props.flexWrap};
  justify-content: ${(props) => props.justifyContent};
  align-items: ${(props) => props.alignItems};
  align-content: ${(props) => props.alignContent};
  order: ${(props) => props.order};
  flex: ${(props) => props.flex};
  flex-grow: ${(props) => props.flexGrow};
  flex-shrink: ${(props) => props.flexShrink};
  align-self: ${(props) => props.alignSelf};
  gap: ${(props) => props.gap};
`;
