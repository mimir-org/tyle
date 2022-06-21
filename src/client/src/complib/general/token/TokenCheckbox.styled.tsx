import * as CheckboxPrimitive from "@radix-ui/react-checkbox";
import styled from "styled-components/macro";
import { TokenBaseProps } from "./Token";
import { tokenBaseStyle } from "./Token.styled";

export const TokenCheckboxContainer = styled(CheckboxPrimitive.Root)<TokenBaseProps>`
  ${tokenBaseStyle};
  outline: ${(props) => props.checked && `2px solid ${props.theme.tyle.color.sys.surface.on}`};
`;

TokenCheckboxContainer.defaultProps = {
  variant: "primary",
};
