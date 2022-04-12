import styled from "styled-components/macro";
import { ElementType } from "react";
import { FlexboxProps } from "./Flexbox";

export const FlexboxContainer = styled.div<FlexboxProps<ElementType>>`
  display: flex;
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
