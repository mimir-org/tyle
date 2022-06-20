import { motion } from "framer-motion";
import { ElementType } from "react";
import styled from "styled-components/macro";
import { displayMixin, ellipsisMixin, getTextRole, paletteMixin, spacingMixin, typographyMixin } from "../mixins";
import { Display, Palette, Polymorphic, Spacing, TextVariant, Typography } from "../props";
import { Ellipsis } from "../props/ellipsis";

type HeadingProps = Spacing &
  Pick<Palette, "color"> &
  Pick<Display, "whiteSpace" | "display"> &
  Pick<Typography, "font" | "fontSize" | "fontWeight" | "textAlign" | "textTransform"> &
  Polymorphic<ElementType> &
  TextVariant &
  Ellipsis;

/**
 * A polymorphic component for heading elements
 *
 * @param as element to display component as (defaults to <h1>)
 * @param font overrides font of text element
 * @param fontSize overrides default size of the text element
 * @param fontWeight overrides default font-weight of the text element
 * @param color overrides default color of the text element
 * @param useEllipsis enable truncation of text
 * @param ellipsisMaxLines set how many lines to display before truncation
 * @constructor
 */
export const Heading = styled.h1<HeadingProps>`
  ${({ variant }) => getTextRole(variant)}};
  ${paletteMixin};
  ${displayMixin};
  ${spacingMixin};
  ${ellipsisMixin};
  ${typographyMixin};
`;

Heading.defaultProps = {
  useEllipsis: false,
  ellipsisMaxLines: 1,
};

/**
 * An animation wrapper for the Text component
 *
 * @see https://github.com/framer/motion
 */
export const MotionHeading = motion(Heading, { forwardMotionProps: true });
