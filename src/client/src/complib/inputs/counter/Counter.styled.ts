import { focusRaw, getTextRole, sizingMixin } from "complib/mixins";
import styled from "styled-components/macro";

export const CounterContainer = styled.div`
  display: flex;
  align-items: center;
  gap: ${(props) => props.theme.tyle.spacing.l};

  width: 120px;
  height: 40px;

  border: 1px solid ${(props) => props.theme.tyle.color.sys.outline.base};
  border-radius: ${(props) => props.theme.tyle.border.radius.medium};
  padding: ${(props) => props.theme.tyle.spacing.base};

  background-color: ${(props) => props.theme.tyle.color.sys.pure.base};

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
