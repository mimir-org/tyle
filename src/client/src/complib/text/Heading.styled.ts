import styled from "styled-components/macro";
import { HeadingProps } from "./Heading";
import { ElementType } from "react";

export const HeadingContainer = styled.h1<HeadingProps<ElementType>>`
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
