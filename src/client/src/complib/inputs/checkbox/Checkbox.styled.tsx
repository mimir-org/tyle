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
  ${focus};
`;

export const CheckboxIndicator = styled(CheckboxPrimitive.Indicator)``;

export const CheckboxChecked = styled(CheckboxCheckedIcon)`
  position: absolute;
  inset: 0;
`;
