import { focus, getTextRole, placeholder, sizingMixin } from "complib/mixins";
import { Sizing } from "complib/props";
import { TextareaHTMLAttributes } from "react";
import styled from "styled-components/macro";

/**
 * A simple wrapper over the textarea-tag, with styling that follows library conventions.
 */

type TextareaProps = TextareaHTMLAttributes<HTMLTextAreaElement> & Sizing;

export const Textarea = styled.textarea<TextareaProps>`
  border: 1px solid ${(props) => props.theme.tyle.color.sys.outline.base};
  border-radius: ${(props) => props.theme.tyle.border.radius.medium};
  min-height: 150px;
  width: 100%;
  padding: ${(props) => props.theme.tyle.spacing.base};
  color: ${(props) => props.theme.tyle.color.sys.surface.on};

  background-color: ${(props) => props.theme.tyle.color.sys.pure.base};
  color: ${(props) => props.theme.tyle.color.sys.background.on};

  ${getTextRole("body-large")};
  ${focus};
  ${placeholder};
  ${sizingMixin};
`;
