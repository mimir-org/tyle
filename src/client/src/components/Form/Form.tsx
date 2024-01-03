import { FormHTMLAttributes } from "react";
import { flexMixin } from "styleConstants";
import styled from "styled-components";
import { Flex } from "types/styleProps";

type FormProps = FormHTMLAttributes<HTMLFormElement> & Flex;

/**
 * A simple wrapper around form to control general form layout
 */
const Form = styled.form<FormProps>`
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  gap: ${(props) => props.theme.tyle.spacing.xxxl};
  width: 100%;

  ${flexMixin};
`;

export default Form;
