import { sizingMixin, translucify } from "complib/mixins";
import { Sizing } from "complib/props";
import { motion } from "framer-motion";
import styled from "styled-components/macro";

export type TooltipContentProps = Sizing;

const TooltipContent = styled.div<TooltipContentProps>`
  padding: ${(props) => props.theme.mimirorg.spacing.base} ${(props) => props.theme.mimirorg.spacing.l};
  border-radius: ${(props) => props.theme.mimirorg.border.radius.large};
  background-color: ${(props) => translucify(props.theme.mimirorg.color.primary.base, 0.9)};
  color: ${(props) => props.theme.mimirorg.color.primary.on};
  box-shadow: ${(props) => props.theme.mimirorg.shadow.small};
  ${sizingMixin};
`;

export const MotionTooltipContent = motion(TooltipContent);
