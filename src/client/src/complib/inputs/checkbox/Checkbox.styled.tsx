import * as CheckboxPrimitive from "@radix-ui/react-checkbox";
import styled from "styled-components/macro";
import { CheckboxCheckedIcon } from "../../../assets/icons/checkmark";
import { focus } from "../../mixins";

export const CheckboxRoot = styled(CheckboxPrimitive.Root)`
  all: unset;
  position: relative;
  border-radius: ${(props) => props.theme.tyle.border.radius.small};
  color: ${(props) => props.theme.tyle.color.sys.primary.base};
  height: 24px;
  width: 24px;

  :disabled {
    color: ${(props) => props.theme.tyle.color.sys.surface.variant.on};
  }

  :not(:disabled) {
    :hover {
      background-color: ${(props) => props.theme.tyle.color.sys.secondary.base};
    }

    :active {
      color: ${(props) => props.theme.tyle.color.sys.surface.on};
    }
  }

  ${focus};
`;

export const CheckboxIndicator = styled(CheckboxPrimitive.Indicator)``;

export const CheckboxChecked = styled(CheckboxCheckedIcon)`
  position: absolute;
  inset: 0;
`;
