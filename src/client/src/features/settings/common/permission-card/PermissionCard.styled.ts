import { Card } from "complib/surfaces";
import { motion } from "framer-motion";
import styled from "styled-components/macro";

const PermissionCardContainer = styled(Card).attrs(() => ({
  as: "article",
}))`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.mimirorg.spacing.xl};
  max-width: 350px;
`;

export const MotionPermissionCardContainer = motion(PermissionCardContainer);
