import { InputProps } from "complib/inputs/input/Input";
import { focus, getTextRole, placeholder, sizingMixin } from "complib/mixins";
import styled from "styled-components";

export const InputContainer = styled.input<InputProps>`
  height: 100%;
  width: 100%;

  border: 1px solid ${(props) => props.theme.mimirorg.color.outline.base};
  border-radius: ${(props) => props.theme.mimirorg.border.radius.medium};
  padding: ${(props) => props.theme.mimirorg.spacing.base} ${(props) => props.theme.mimirorg.spacing.l};
  padding-right: ${(props) => props.icon && props.iconPlacement === "right" && props.theme.mimirorg.spacing.multiple(6)};
  padding-left: ${(props) => props.icon && props.iconPlacement === "left" && props.theme.mimirorg.spacing.multiple(6)};

  background-color: ${(props) => props.theme.mimirorg.color.pure.base};
  color: ${(props) => props.theme.mimirorg.color.background.on};

  :disabled {
    color: ${(props) => props.theme.mimirorg.color.surface.variant.on};
    background-color: ${(props) => props.theme.mimirorg.color.outline.base};
  }

  ${getTextRole("body-large")};
  ${sizingMixin};
  ${placeholder};
  ${focus};
`;

export const InputIconContainer = styled.span<InputProps>`
  position: absolute;
  top: 50%;
  transform: translate(0, -50%);
  left: ${(props) => props.iconPlacement === "left" && props.theme.mimirorg.spacing.xl};
  right: ${(props) => props.iconPlacement === "right" && props.theme.mimirorg.spacing.xl};
  color: ${(props) => props.theme.mimirorg.color.primary.base};
  line-height: 0;

  img,
  svg {
    width: 24px;
    height: 24px;
  }
`;
