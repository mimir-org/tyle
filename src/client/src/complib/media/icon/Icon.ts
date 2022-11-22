import { sizingMixin } from "complib/mixins";
import { Sizing } from "complib/props";
import { motion } from "framer-motion";
import { ImgHTMLAttributes } from "react";
import styled from "styled-components";

export type IconProps = ImgHTMLAttributes<HTMLImageElement> &
  Sizing & {
    size?: number;
  };

/**
 * A simple wrapper over the img-tag
 * Has a default width and height of 1em
 * @param size sets height and width of icon
 */
export const Icon = styled.img<IconProps>`
  display: inline-block;
  width: ${(props) => (props.size ? `${props.size}px` : "1em")};
  height: ${(props) => (props.size ? `${props.size}px` : "1em")};
  line-height: 1;
  ${sizingMixin};
`;

/**
 * An animation wrapper for the Icon component
 *
 * @see https://github.com/framer/motion
 */
export const MotionIcon = motion(Icon);
