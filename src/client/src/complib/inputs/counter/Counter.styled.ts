import { focusRaw, getTextRole, sizingMixin } from "complib/mixins";
import styled, { css } from "styled-components/macro";

interface CounterContainerProps {
  disabled?: boolean;
}

export const CounterContainer = styled.div<CounterContainerProps>`
  display: flex;
  align-items: center;
  gap: ${(props) => props.theme.mimirorg.spacing.l};

  width: 120px;
  height: 40px;

  border: 1px solid ${(props) => props.theme.mimirorg.color.outline.base};
  border-radius: ${(props) => props.theme.mimirorg.border.radius.medium};
  padding: ${(props) => props.theme.mimirorg.spacing.base};

  background-color: ${(props) => props.theme.mimirorg.color.pure.base};

  ${({ disabled }) =>
    disabled &&
    css`
      color: ${(props) => props.theme.mimirorg.color.surface.variant.on};
      background-color: ${(props) => props.theme.mimirorg.color.outline.base};
    `};

  :focus-within {
    ${focusRaw};
  }

  ${sizingMixin};
`;

export const CounterInput = styled.input`
  all: unset;
  appearance: textfield;

  width: 32px;
  height: 24px;

  ::-webkit-inner-spin-button,
  ::-webkit-outer-spin-button {
    -webkit-appearance: none;
  }

  text-align: center;
  ${getTextRole("body-large")};
`;
