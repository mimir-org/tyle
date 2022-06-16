import styled from "styled-components/macro";
import { motion } from "framer-motion";

const TooltipContent = styled.div`
  padding: ${(props) => props.theme.tyle.spacing.xs};
  border-radius: ${(props) => props.theme.tyle.border.radius.medium};
  background-color: ${(props) => props.theme.tyle.color.sys.surface.inverse.base};
  color: ${(props) => props.theme.tyle.color.sys.surface.inverse.on};
`;

export const MotionTooltipContent = motion(TooltipContent);
