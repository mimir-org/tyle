import { motion } from "framer-motion";
import styled from "styled-components/macro";
import { focus, paletteMixin, sizingMixin, translucify } from "../../mixins";
import { Palette, Sizing } from "../../props";

export type PopoverContentProps = Sizing & Palette;

const PopoverContent = styled.div<PopoverContentProps>`
  padding: ${(props) => props.theme.tyle.spacing.xl};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  background-color: ${(props) => translucify(props.theme.tyle.color.sys.primary.base, 0.9)};
  color: ${(props) => props.theme.tyle.color.sys.primary.on};
  box-shadow: ${(props) => props.theme.tyle.shadow.small};
  ${sizingMixin};
  ${paletteMixin};
  ${focus};
`;

export const MotionPopoverContent = motion(PopoverContent);
