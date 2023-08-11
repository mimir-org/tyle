import { getTextRole } from "complib/mixins";
import styled, { css } from "styled-components/macro";

interface FormFieldLabelTextProps {
  indent?: boolean;
}

export const FormFieldLabelText = styled.span<FormFieldLabelTextProps>`
  ${getTextRole("label-large")}
  color: ${(props) => props.theme.mimirorg.color.surface.variant.on};

  ${({ indent }) =>
    indent &&
    css`
      padding-left: ${(props) => props.theme.mimirorg.spacing.l};
      border-left: 1px solid transparent;
    `}
`;
