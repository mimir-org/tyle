import { hideScrollbar } from "complib/mixins";
import { motion } from "framer-motion";
import styled from "styled-components/macro";

const ItemListContainer = styled.div`
  position: relative;
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.xxxl};

  height: 100%;

  // Fade bottom of container
  mask-image: linear-gradient(to bottom, black 95%, transparent 100%);

  // Extra space for animated content
  padding-top: ${(props) => props.theme.tyle.spacing.xl};
  padding-left: ${(props) => props.theme.tyle.spacing.l};
  padding-right: ${(props) => props.theme.tyle.spacing.l};
  margin-top: ${(props) => `-${props.theme.tyle.spacing.xl}`};
  margin-left: ${(props) => `-${props.theme.tyle.spacing.l}`};
  margin-right: ${(props) => `-${props.theme.tyle.spacing.l}`};

  // Hidden scrollbar
  overflow-y: auto;
  ${hideScrollbar};
`;

export const MotionItemListContainer = motion(ItemListContainer);
