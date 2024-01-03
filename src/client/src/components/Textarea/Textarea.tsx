import { getTextRole } from "helpers/theme.helpers";
import { TextareaHTMLAttributes } from "react";
import { focus, placeholder, sizingMixin } from "styleConstants";
import styled from "styled-components";
import { Sizing } from "types/styleProps";

/**
 * A simple wrapper over the textarea-tag, with styling that follows library conventions.
 */

type TextareaProps = TextareaHTMLAttributes<HTMLTextAreaElement> & Sizing;

const Textarea = styled.textarea<TextareaProps>`
  border: 1px solid ${(props) => props.theme.tyle.color.outline.base};
  border-radius: ${(props) => props.theme.tyle.border.radius.medium};
  min-height: 150px;
  width: 100%;
  padding: ${(props) => props.theme.tyle.spacing.base};
  color: ${(props) => props.theme.tyle.color.surface.on};

  background-color: ${(props) => props.theme.tyle.color.pure.base};
  color: ${(props) => props.theme.tyle.color.background.on};

  :disabled {
    color: ${(props) => props.theme.tyle.color.surface.variant.on};
    background-color: ${(props) => props.theme.tyle.color.outline.base};
  }

  ${getTextRole("body-large")};
  ${focus};
  ${placeholder};
  ${sizingMixin};
`;

export default Textarea;
