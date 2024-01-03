import { getTextRole } from "helpers/theme.helpers";
import styled from "styled-components";
import { TextTypes } from "types/styleProps";

interface FormFieldLabelTextProps {
  indent?: boolean;
  variant: TextTypes;
}

export const FormFieldLabelText = styled.span<FormFieldLabelTextProps>`
  ${({ variant }) => getTextRole(variant)}
  color: ${(props) => props.theme.tyle.color.surface.variant.on};
  padding-left: ${(props) => props.indent && props.theme.tyle.spacing.l};
  border-left: ${(props) => props.indent && "1px solid transparent"};
`;
