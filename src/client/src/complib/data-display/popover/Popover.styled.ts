import styled from "styled-components/macro";
import { motion } from "framer-motion";

const PopoverContent = styled.div`
  padding: ${(props) => props.theme.tyle.spacing.small};
  border-radius: ${(props) => props.theme.tyle.border.radius.medium};
  background-color: ${(props) => props.theme.tyle.color.sys.surface.inverse.base};
  color: ${(props) => props.theme.tyle.color.sys.surface.inverse.on};
`;

export const MotionPopoverContent = motion(PopoverContent);
