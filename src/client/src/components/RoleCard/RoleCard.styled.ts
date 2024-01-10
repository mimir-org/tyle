import Card from "components/Card";
import { motion } from "framer-motion";
import styled from "styled-components/macro";

const RoleCardContainer = styled(Card).attrs(() => ({
  as: "article",
}))`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.xl};
  max-width: 350px;
`;

const MotionRoleCardContainer = motion(RoleCardContainer);

export default MotionRoleCardContainer;
