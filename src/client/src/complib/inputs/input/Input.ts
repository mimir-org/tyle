import { InputHTMLAttributes } from "react";
import styled from "styled-components";
import { focus, getTextRole, placeholder, sizingMixin } from "../../mixins";
import { Sizing } from "../../props";

type InputProps = InputHTMLAttributes<HTMLInputElement> & Sizing;

/**
 * A simple wrapper over the input-tag, with styling that follows library conventions.
 */
export const Input = styled.input<InputProps>`
  height: 40px;
  padding: ${(props) => props.theme.tyle.spacing.base} ${(props) => props.theme.tyle.spacing.l};
  border: 1px solid ${(props) => props.theme.tyle.color.sys.outline.base};
  border-radius: ${(props) => props.theme.tyle.border.radius.medium};

  background-color: ${(props) => props.theme.tyle.color.sys.pure.base};
  color: ${(props) => props.theme.tyle.color.sys.background.on};

  ${getTextRole("body-large")};
  ${sizingMixin};
  ${focus};
  ${placeholder};
`;
