import { focusRaw, getTextRole, sizingMixin, translucify } from "complib/mixins";
import styled, { css } from "styled-components/macro";

interface CounterContainerProps {
  disabled?: boolean;
}

export const CounterContainer = styled.div<CounterContainerProps>`
  display: flex;
  align-items: center;
  gap: ${(props) => props.theme.tyle.spacing.l};

  width: 120px;
  height: 40px;

  border: 1px solid ${(props) => props.theme.tyle.color.sys.outline.base};
  border-radius: ${(props) => props.theme.tyle.border.radius.medium};
  padding: ${(props) => props.theme.tyle.spacing.base};

  background-color: ${(props) => props.theme.tyle.color.sys.pure.base};

  ${({ disabled }) =>
    disabled &&
    css`
      background-color: ${(props) => translucify(props.theme.tyle.color.sys.surface.on, 0.08)};
    `}

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

  :disabled {
    color: ${(props) => props.theme.tyle.color.sys.surface.variant.on};
  }

  text-align: center;
  ${getTextRole("body-large")};
`;
