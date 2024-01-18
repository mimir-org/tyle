import { motion } from "framer-motion";
import { translucify } from "helpers/theme.helpers";
import { sizingMixin } from "styleConstants";
import styled from "styled-components";
import { Sizing } from "types/styleProps";

export type TooltipContentProps = Sizing;

const TooltipContent = styled.div<TooltipContentProps>`
  padding: ${(props) => props.theme.tyle.spacing.base} ${(props) => props.theme.tyle.spacing.l};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  background-color: ${(props) => translucify(props.theme.tyle.color.primary.base, 0.9)};
  color: ${(props) => props.theme.tyle.color.primary.on};
  box-shadow: ${(props) => props.theme.tyle.shadow.small};
  ${sizingMixin};
`;

export const MotionTooltipContent = motion(TooltipContent);
