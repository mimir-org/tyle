import { flexMixin } from "styleConstants";
import styled from "styled-components";
import { Flex } from "types/styleProps";

export type FormFieldsetProps = Flex;

/**
 * A simple wrapper around fieldset to control padding/margins/spacing around form inputs
 */
const FormFieldset = styled.fieldset<FormFieldsetProps>`
  display: flex;
  gap: ${(props) => props.theme.tyle.spacing.xxl};

  padding: ${(props) => props.theme.tyle.spacing.xl} ${(props) => props.theme.tyle.spacing.multiple(6)};

  border: 0;
  border-radius: ${(props) => props.theme.tyle.border.radius.medium};
  box-shadow: ${(props) => props.theme.tyle.shadow.medium};
  background-color: ${(props) => props.theme.tyle.color.text.on};

  ${flexMixin};
`;

FormFieldset.defaultProps = {
  flexDirection: "column",
};

export default FormFieldset;
