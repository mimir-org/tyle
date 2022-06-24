import { motion } from "framer-motion";
import styled from "styled-components/macro";
import { MotionBox } from "../../layouts";
import { flexMixin, sizingMixin, translucify } from "../../mixins";
import { Flex, Sizing } from "../../props";

export type DialogContentProps = Sizing & Flex;

export const DialogContent = styled(MotionBox)<DialogContentProps>`
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);

  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: ${(props) => props.theme.tyle.spacing.xxxl};

  background-color: ${(props) => props.theme.tyle.color.sys.background.base};
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

export const DialogOverlay = styled(motion.div)`
  position: fixed;
  inset: 0;
  background-color: ${(props) => translucify(props.theme.tyle.color.sys.primary.base, 0.08)};
`;
