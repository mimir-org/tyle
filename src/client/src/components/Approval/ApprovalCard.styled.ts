import Card from "components/Card";
import { motion } from "framer-motion";
import styled from "styled-components/macro";

const ApprovalCardContainer = styled(Card).attrs(() => ({
  as: "article",
}))`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.xl};
  max-width: 350px;
  min-width: 300px;
`;

const MotionApprovalCardContainer = motion(ApprovalCardContainer);

export default MotionApprovalCardContainer;
