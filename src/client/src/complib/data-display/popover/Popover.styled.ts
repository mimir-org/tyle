import { focus, paletteMixin, sizingMixin, translucify } from "complib/mixins";
import { Palette, Sizing } from "complib/props";
import { motion } from "framer-motion";
import styled from "styled-components/macro";

export type PopoverContentProps = Sizing & Palette;

const PopoverContent = styled.div<PopoverContentProps>`
  padding: ${(props) => props.theme.mimirorg.spacing.xl};
  border-radius: ${(props) => props.theme.mimirorg.border.radius.large};
  background-color: ${(props) => translucify(props.theme.mimirorg.color.primary.base, 0.9)};
  color: ${(props) => props.theme.mimirorg.color.primary.on};
  box-shadow: ${(props) => props.theme.mimirorg.shadow.small};
  ${sizingMixin};
  ${paletteMixin};
  ${focus};
`;

export const MotionPopoverContent = motion(PopoverContent);
