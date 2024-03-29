import { MotionBox } from "components/Box";
import { motion } from "framer-motion";
import { translucify } from "helpers/theme.helpers";
import { flexMixin, sizingMixin } from "styleConstants";
import styled from "styled-components";
import { Flex, Sizing } from "types/styleProps";

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
  gap: ${(props) => props.theme.tyle.spacing.xxxl};

  background-color: ${(props) => props.theme.tyle.color.background.base};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};

  width: 590px;
  min-height: 380px;
  max-width: 90vw;
  max-height: 85vh;
  padding: ${(props) => props.theme.tyle.spacing.multiple(6)};

  box-shadow: ${(props) => props.theme.tyle.shadow.small};

  ${sizingMixin};
  ${flexMixin};
`;

export const AlertDialogOverlay = styled(motion.div)`
  position: fixed;
  inset: 0;
  background-color: ${(props) => translucify(props.theme.tyle.color.surface.on, 0.08)};
`;
