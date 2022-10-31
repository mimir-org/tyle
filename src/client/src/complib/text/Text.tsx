import {
  displayMixin,
  ellipsisMixin,
  getTextRole,
  paletteMixin,
  sizingMixin,
  spacingMixin,
  typographyMixin,
} from "complib/mixins";
import { Display, Ellipsis, Palette, Polymorphic, Sizing, Spacing, TextVariant, Typography } from "complib/props";
import { motion } from "framer-motion";
import { ElementType } from "react";
import styled from "styled-components/macro";

type TextProps = Spacing &
  Sizing &
  Pick<Palette, "color"> &
  Pick<Display, "whiteSpace" | "display"> &
  Pick<Typography, "font" | "fontSize" | "fontWeight" | "textAlign" | "textTransform" | "wordBreak"> &
  Polymorphic<ElementType> &
  TextVariant &
  Ellipsis;

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
  ${({ variant }) => getTextRole(variant)}
  ${paletteMixin}
  ${displayMixin}
  ${spacingMixin}
  ${ellipsisMixin}
  ${typographyMixin}
  ${sizingMixin}
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
export const MotionText = motion(Text);
