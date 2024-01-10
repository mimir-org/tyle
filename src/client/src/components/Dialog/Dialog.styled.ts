import Box from "components/Box";
import { motion } from "framer-motion";
import { translucify } from "helpers/theme.helpers";
import { flexMixin, sizingMixin } from "styleConstants";
import styled from "styled-components";
import { Flex, Sizing } from "types/styleProps";

export interface DialogContentProps extends Sizing, Flex {}

export const DialogContent = styled(Box)<DialogContentProps>`
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

  min-height: 380px;
  padding: ${(props) => props.theme.tyle.spacing.multiple(6)};

  box-shadow: ${(props) => props.theme.tyle.shadow.small};

  ${sizingMixin};
  ${flexMixin};
`;

export const DialogOverlay = styled(motion.div)`
  position: fixed;
  inset: 0;
  background-color: ${(props) => translucify(props.theme.tyle.color.primary.base, 0.08)};
`;
