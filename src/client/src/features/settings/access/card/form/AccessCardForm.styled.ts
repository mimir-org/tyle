import { motion } from "framer-motion";
import styled from "styled-components/macro";

const UndoToasterContainer = styled.div`
  display: flex;
  align-items: center;
  gap: ${(props) => props.theme.tyle.spacing.l};

  padding: ${(props) => props.theme.tyle.spacing.base} ${(props) => props.theme.tyle.spacing.l};
  border-radius: ${(props) => props.theme.tyle.border.radius.medium};

  color: ${(props) => props.theme.tyle.color.sys.warning.on};
  background-color: ${(props) => props.theme.tyle.color.sys.warning.base};

  box-shadow: ${(props) => props.theme.tyle.shadow.small};
`;

export const MotionUndoToasterContainer = motion(UndoToasterContainer);
