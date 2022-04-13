import styled from "styled-components/macro";
import { ElementType } from "react";
import { motion } from "framer-motion";
import { Palette, Polymorphic, Typography } from "../props";

type TextProps = Pick<Palette, "color"> &
  Pick<Typography, "font" | "fontSize" | "fontWeight"> &
  Polymorphic<ElementType> & {
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
  font: ${(props) => props.font};
  font-size: ${(props) => props.fontSize};
  font-weight: ${(props) => props.fontWeight};
  color: ${(props) => props.color};

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
