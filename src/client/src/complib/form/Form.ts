import { flexMixin } from "complib/mixins";
import { Flex } from "complib/props";
import { FormHTMLAttributes } from "react";
import styled from "styled-components";

type FormProps = FormHTMLAttributes<HTMLFormElement> & Flex;

/**
 * A simple wrapper around form to control general form layout
 */
export const Form = styled.form<FormProps>`
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  gap: ${(props) => props.theme.tyle.spacing.xxxl};
  width: 100%;

  ${flexMixin};
`;
