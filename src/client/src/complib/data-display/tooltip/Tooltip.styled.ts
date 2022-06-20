import { motion } from "framer-motion";
import styled from "styled-components/macro";
import { translucify } from "../../mixins";

const TooltipContent = styled.div`
  padding: ${(props) => props.theme.tyle.spacing.base} ${(props) => props.theme.tyle.spacing.l};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  background-color: ${(props) => translucify(props.theme.tyle.color.sys.primary.base, 0.85)};
  color: ${(props) => props.theme.tyle.color.sys.surface.inverse.on};
  box-shadow: ${(props) => props.theme.tyle.shadow.small};
`;

export const MotionTooltipContent = motion(TooltipContent);
