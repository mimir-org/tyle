import styled from "styled-components/macro";
import { motion } from "framer-motion";

const TooltipContent = styled.div`
  padding: ${(props) => props.theme.typeLibrary.spacing.xs};
  border-radius: ${(props) => props.theme.typeLibrary.border.radius.medium};
  background-color: ${(props) => props.theme.typeLibrary.color.surface.inverse.base};
  color: ${(props) => props.theme.typeLibrary.color.surface.inverse.on};
`;

export const MotionTooltipContent = motion(TooltipContent);
