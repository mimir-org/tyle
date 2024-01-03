import { ImgHTMLAttributes } from "react";
import { sizingMixin } from "styleConstants";
import styled from "styled-components";
import { Sizing } from "types/styleProps";

export type IconProps = ImgHTMLAttributes<HTMLImageElement> &
  Sizing & {
    size?: number;
  };

/**
 * A simple wrapper over the img-tag
 * Has a default width and height of 1em
 * @param size sets height and width of icon
 */
const Icon = styled.img<IconProps>`
  display: inline-block;
  width: ${(props) => (props.size ? `${props.size}px` : "1em")};
  height: ${(props) => (props.size ? `${props.size}px` : "1em")};
  line-height: 1;
  ${sizingMixin};
`;

export default Icon;
