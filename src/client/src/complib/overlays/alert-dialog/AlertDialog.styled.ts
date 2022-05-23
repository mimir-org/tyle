import styled from "styled-components/macro";
import { motion } from "framer-motion";
import { MotionCard } from "../../surfaces";

export const AlertDialogContent = styled(MotionCard)`
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

export const AlertDialogOverlay = styled(motion.div)`
  position: fixed;
  inset: 0;
  background-color: hsla(0, 0%, 0%, 0.5);
`;
