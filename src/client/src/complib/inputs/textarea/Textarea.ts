import { focus, getTextRole, placeholder, sizingMixin } from "complib/mixins";
import { Sizing } from "complib/props";
import { TextareaHTMLAttributes } from "react";
import styled from "styled-components/macro";

/**
 * A simple wrapper over the textarea-tag, with styling that follows library conventions.
 */

type TextareaProps = TextareaHTMLAttributes<HTMLTextAreaElement> & Sizing;

export const Textarea = styled.textarea<TextareaProps>`
  border: 1px solid ${(props) => props.theme.mimirorg.color.outline.base};
  border-radius: ${(props) => props.theme.mimirorg.border.radius.medium};
  min-height: 150px;
  width: 100%;
  padding: ${(props) => props.theme.mimirorg.spacing.base};
  color: ${(props) => props.theme.mimirorg.color.surface.on};

  background-color: ${(props) => props.theme.mimirorg.color.pure.base};
  color: ${(props) => props.theme.mimirorg.color.background.on};

  ${getTextRole("body-large")};
  ${focus};
  ${placeholder};
  ${sizingMixin};
`;
