import styled from "styled-components/macro";
import { MotionCard } from "../../surfaces";
import { motion } from "framer-motion";

export const DialogContent = styled(MotionCard)`
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);

  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.medium};

  width: 90vw;
  max-width: 500px;
  max-height: 85vh;
  padding: ${(props) => props.theme.tyle.spacing.medium};
`;

export const DialogOverlay = styled(motion.div)`
  position: fixed;
  inset: 0;
  background-color: hsla(0, 0%, 0%, 0.5);
`;
