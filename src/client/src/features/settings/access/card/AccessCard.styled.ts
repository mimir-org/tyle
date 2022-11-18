import { Card } from "complib/surfaces";
import { motion } from "framer-motion";
import styled from "styled-components/macro";

const AccessCardContainer = styled(Card).attrs(() => ({
  as: "article",
}))`
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.xl};
  max-width: 350px;
`;

export const MotionAccessCardContainer = motion(AccessCardContainer);
