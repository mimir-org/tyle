import { motion } from "framer-motion";
import { getTextRole } from "helpers/theme.helpers";
import { ElementType } from "react";
import { displayMixin, ellipsisMixin, paletteMixin, sizingMixin, spacingMixin, typographyMixin } from "styleConstants";
import styled from "styled-components";
import { Display, Ellipsis, Palette, Polymorphic, Sizing, Spacings, TextVariant, Typography } from "types/styleProps";

export type TextProps = Spacings &
  Sizing &
  Pick<Palette, "color" | "bgColor"> &
  Pick<Display, "whiteSpace" | "display" | "overflow" | "textOverflow" | "visibility"> &
  Pick<
    Typography,
    | "font"
    | "fontSize"
    | "fontWeight"
    | "textAlign"
    | "textTransform"
    | "wordBreak"
    | "fontFamily"
    | "fontStyle"
    | "letterSpacing"
    | "lineHeight"
  > &
  Polymorphic<ElementType> &
  TextVariant &
  Ellipsis & {
    htmlFor?: string;
  };

/**
 * A polymorphic text component for non-heading text
 *
 * @param as element to display component as (defaults to <p>)
 * @param font overrides font of text element
 * @param fontSize overrides default size of the text element
 * @param fontWeight overrides default font-weight of the text element
 * @param color overrides default color of the text element
 * @constructor
 */
const Text = styled.p<TextProps>`
  ${({ variant }) => getTextRole(variant)};
  ${spacingMixin};
  ${paletteMixin};
  ${displayMixin};
  ${ellipsisMixin};
  ${typographyMixin};
  ${sizingMixin};
`;

Text.displayName = "Text";

Text.defaultProps = {
  useEllipsis: false,
  ellipsisMaxLines: 1,
  htmlFor: "",
};

export default Text;

/**
 * An animation wrapper for the Text component
 *
 * @see https://github.com/framer/motion
 */
export const MotionText = motion(Text);
