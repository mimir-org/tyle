import { InputHTMLAttributes } from "react";
import styled from "styled-components";
import { getTextRole, sizingMixin } from "../../mixins";
import { Sizing } from "../../props";

type InputProps = InputHTMLAttributes<HTMLInputElement> & Sizing;

/**
 * A simple wrapper over the input-tag, with styling that follows library conventions.
 */
export const Input = styled.input<InputProps>`
  height: 40px;
  min-width: 250px;

  padding: ${(props) => props.theme.tyle.spacing.xs};
  border: 1px solid ${(props) => props.theme.tyle.color.outline.base};
  border-radius: ${(props) => props.theme.tyle.border.radius.small};

  ${getTextRole("body-large")};
  ${sizingMixin};

  ::placeholder {
    color: ${(props) => props.theme.tyle.color.outline.base};
  }
`;
