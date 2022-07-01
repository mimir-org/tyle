import styled from "styled-components";
import { focus, getTextRole, placeholder, sizingMixin } from "../../mixins";
import { InputProps } from "./Input";

export const InputContainer = styled.input<InputProps>`
  height: 100%;
  width: 100%;

  padding: ${(props) => props.theme.tyle.spacing.base} ${(props) => props.theme.tyle.spacing.l};
  padding-left: ${(props) => props.iconPlacement === "left" && props.theme.tyle.spacing.multiple(6)};
  border: 1px solid ${(props) => props.theme.tyle.color.sys.outline.base};
  border-radius: ${(props) => props.theme.tyle.border.radius.medium};

  background-color: ${(props) => props.theme.tyle.color.sys.pure.base};
  color: ${(props) => props.theme.tyle.color.sys.background.on};

  ${getTextRole("body-large")};
  ${sizingMixin};
  ${placeholder};
  ${focus};
`;

export const InputIconContainer = styled.span<InputProps>`
  position: absolute;
  top: 50%;
  transform: translate(0, -50%);
  left: ${(props) => props.iconPlacement === "left" && props.theme.tyle.spacing.xl};
  right: ${(props) => props.iconPlacement === "right" && props.theme.tyle.spacing.xl};
  color: ${(props) => props.theme.tyle.color.sys.primary.base};
  line-height: 0;

  img,
  svg {
    width: 24px;
    height: 24px;
  }
`;
