import { focus, getTextRole, placeholder } from "complib/mixins";
import styled from "styled-components/macro";

/**
 * A simple wrapper over the textarea-tag, with styling that follows library conventions.
 */
export const Textarea = styled.textarea`
  border: 1px solid ${(props) => props.theme.tyle.color.sys.outline.base};
  border-radius: ${(props) => props.theme.tyle.border.radius.medium};
  min-height: 150px;
  padding: ${(props) => props.theme.tyle.spacing.base};
  color: ${(props) => props.theme.tyle.color.sys.surface.on};

  background-color: ${(props) => props.theme.tyle.color.sys.pure.base};
  color: ${(props) => props.theme.tyle.color.sys.background.on};

  ${getTextRole("body-large")};
  ${focus};
  ${placeholder};
`;
