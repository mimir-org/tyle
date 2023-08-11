import * as CheckboxPrimitive from "@radix-ui/react-checkbox";
import { CheckboxCheckedIcon } from "complib/inputs/checkbox/assets";
import { focus } from "complib/mixins";
import { motion } from "framer-motion";
import styled from "styled-components/macro";

export const CheckboxRoot = styled(CheckboxPrimitive.Root)`
  all: unset;
  position: relative;
  border-radius: ${(props) => props.theme.mimirorg.border.radius.small};
  color: ${(props) => props.theme.mimirorg.color.primary.base};
  height: 24px;
  width: 24px;

  :disabled {
    color: ${(props) => props.theme.mimirorg.color.surface.variant.on};
    cursor: not-allowed;
  }

  :not(:disabled) {
    :hover {
      background-color: ${(props) => props.theme.mimirorg.color.secondary.base};
    }

    :active {
      color: ${(props) => props.theme.mimirorg.color.surface.on};
    }
  }

  ${focus};
`;

export const CheckboxIndicator = styled(CheckboxPrimitive.Indicator)``;

export const CheckboxChecked = styled(CheckboxCheckedIcon)`
  position: absolute;
  inset: 0;
`;

/**
 * An animation wrapper for the CheckboxRoot component
 *
 * @see https://github.com/framer/motion
 */
export const MotionCheckboxRoot = motion(CheckboxRoot);
