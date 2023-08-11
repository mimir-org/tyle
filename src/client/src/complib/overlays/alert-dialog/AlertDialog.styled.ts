import { MotionBox } from "complib/layouts";
import { flexMixin, sizingMixin, translucify } from "complib/mixins";
import { Flex, Sizing } from "complib/props";
import { motion } from "framer-motion";
import styled from "styled-components/macro";

export type AlertDialogContentProps = Sizing & Flex;

export const AlertDialogContent = styled(MotionBox)<AlertDialogContentProps>`
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);

  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: ${(props) => props.theme.mimirorg.spacing.xxxl};

  background-color: ${(props) => props.theme.mimirorg.color.background.base};
  border-radius: ${(props) => props.theme.mimirorg.border.radius.large};

  width: 590px;
  min-height: 380px;
  max-width: 90vw;
  max-height: 85vh;
  padding: ${(props) => props.theme.mimirorg.spacing.multiple(6)};

  box-shadow: ${(props) => props.theme.mimirorg.shadow.small};

  ${sizingMixin};
  ${flexMixin};
`;

export const AlertDialogOverlay = styled(motion.div)`
  position: fixed;
  inset: 0;
  background-color: ${(props) => translucify(props.theme.mimirorg.color.surface.on, 0.08)};
`;
