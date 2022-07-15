import * as CheckboxPrimitive from "@radix-ui/react-checkbox";
import { motion } from "framer-motion";
import styled from "styled-components/macro";
import { TokenBaseProps } from "./Token";
import { tokenBaseStyle } from "./Token.styled";

export const TokenCheckboxContainer = styled(CheckboxPrimitive.Root)<TokenBaseProps>`
  ${tokenBaseStyle};
  background-color: ${(props) => props.checked && props.theme.tyle.color.sys.tertiary.container?.base};
`;

TokenCheckboxContainer.defaultProps = {
  variant: "primary",
};

/**
 * An animation wrapper for the TokenCheckboxContainer component
 *
 * @see https://github.com/framer/motion
 */
export const MotionTokenCheckboxContainer = motion(TokenCheckboxContainer);
