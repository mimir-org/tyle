import styled from "styled-components/macro";
import { TextProps } from "./Text";
import { ElementType } from "react";

export const TextContainer = styled.p<TextProps<ElementType>>`
  font: ${(props) => props.font};
  font-size: ${(props) => props.fontSize};

  // Ellipsis
  ${({ useEllipsis, ellipsisMaxLines }) =>
    useEllipsis &&
    `
    display: -webkit-box;
    -webkit-box-orient: vertical;
    -webkit-line-clamp: ${ellipsisMaxLines};
    overflow: hidden;
  `}
`;
