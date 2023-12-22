import { Card } from "@mimirorg/component-library";
import { motion } from "framer-motion";
import styled from "styled-components/macro";

const RoleCardContainer = styled(Card).attrs(() => ({
  as: "article",
}))`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.mimirorg.spacing.xl};
  max-width: 350px;
`;

const MotionRoleCardContainer = motion(RoleCardContainer);

export default MotionRoleCardContainer;
