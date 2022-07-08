import * as CheckboxPrimitive from "@radix-ui/react-checkbox";
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
