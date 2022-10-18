import { motion } from "framer-motion";
import styled from "styled-components/macro";

export const RegisterVerifyForm = styled.form`
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: space-evenly;
  align-items: center;
`;

export const MotionRegisterVerifyForm = motion(RegisterVerifyForm);
