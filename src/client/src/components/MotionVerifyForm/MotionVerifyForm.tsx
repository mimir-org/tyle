import { motion } from "framer-motion";
import styled from "styled-components/macro";

const VerifyForm = styled.form`
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: space-evenly;
  align-items: center;
  gap: ${(props) => props.theme.tyle.spacing.xxxl};
`;

const MotionVerifyForm = motion(VerifyForm);

export default MotionVerifyForm;
