import { css } from "styled-components/macro";
import { Ellipsis } from "../props/ellipsis";

export const ellipsisMixin = css<Ellipsis>`
  ${({ useEllipsis, ellipsisMaxLines }) =>
    useEllipsis &&
    `
    display: -webkit-box;
    -webkit-box-orient: vertical;
    -webkit-line-clamp: ${ellipsisMaxLines};
    overflow: hidden;
  `}
`;
