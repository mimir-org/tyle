import styled from "styled-components/macro";
import { ElementType } from "react";
import { motion } from "framer-motion";
import { Display, Palette, Polymorphic, TextVariant, Typography } from "../props";
import { displayMixin, paletteMixin, textVariantMixin, typographyMixin } from "../mixins";

type TextProps = Pick<Palette, "color"> &
  Pick<Display, "whiteSpace" | "display"> &
  Pick<Typography, "font" | "fontSize" | "fontWeight"> &
  Polymorphic<ElementType> &
  TextVariant & {
    useEllipsis?: boolean;
    ellipsisMaxLines?: number;
  };

/**
 * A polymorphic text component for non-heading text
 *
 * @param as element to display component as (defaults to <p>)
 * @param font overrides font of text element
 * @param fontSize overrides default size of the text element
 * @param fontWeight overrides default font-weight of the text element
 * @param color overrides default color of the text element
 * @param useEllipsis enable truncation of text
 * @param ellipsisMaxLines set how many lines to display before truncation
 * @constructor
 */
export const Text = styled.p<TextProps>`
  ${typographyMixin};
  ${paletteMixin};
  ${displayMixin};
  ${textVariantMixin};

  ${({ useEllipsis, ellipsisMaxLines }) =>
    useEllipsis &&
    `
    display: -webkit-box;
    -webkit-box-orient: vertical;
    -webkit-line-clamp: ${ellipsisMaxLines};
    overflow: hidden;
  `}
`;

Text.defaultProps = {
  useEllipsis: false,
  ellipsisMaxLines: 1,
};

/**
 * An animation wrapper for the Text component
 *
 * @see https://github.com/framer/motion
 */
export const MotionText = motion(Text, { forwardMotionProps: true });
