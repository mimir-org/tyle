import { css } from "styled-components/macro";
import { TextVariant } from "../props";

export const textVariantMixin = css<TextVariant>`
  ${({ variant }) =>
    variant &&
    `
    font: var(--tl-sys-typescale-${variant});
    letter-spacing: var(--tl-sys-typescale-${variant}-spacing);
    line-height: var(--tl-sys-typescale-${variant}-line-height);
  `};
`;
