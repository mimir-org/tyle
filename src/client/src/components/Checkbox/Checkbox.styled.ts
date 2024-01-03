import * as CheckboxPrimitive from "@radix-ui/react-checkbox";
import { CheckBox, CheckBoxOutlineBlank } from "@styled-icons/material-rounded";
import { motion } from "framer-motion";
import { focus } from "styleConstants";
import styled from "styled-components";

export const CheckboxRoot = styled(CheckboxPrimitive.Root)`
  all: unset;
  position: relative;
  border-radius: ${(props) => props.theme.tyle.border.radius.small};
  color: ${(props) => props.theme.tyle.color.primary.base};
  height: 24px;
  width: 24px;

  :disabled {
    color: ${(props) => props.theme.tyle.color.surface.variant.on};
    cursor: not-allowed;
  }

  :not(:disabled) {
    :hover {
      background-color: ${(props) => props.theme.tyle.color.secondary.base};
    }

    :active {
      color: ${(props) => props.theme.tyle.color.surface.on};
    }
  }

  ${focus};
`;

export const CheckboxIndicator = styled(CheckboxPrimitive.Indicator)``;

export const CheckboxEmpty = styled(CheckBoxOutlineBlank)`
  position: absolute;
  inset: 0;
`;

export const CheckboxChecked = styled(CheckBox)`
  position: absolute;
  inset: 0;
`;

/**
 * An animation wrapper for the CheckboxRoot component
 *
 * @see https://github.com/framer/motion
 */
export const MotionCheckboxRoot = motion(CheckboxRoot);
