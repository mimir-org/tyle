import styled from "styled-components";
import { getTextRole } from "../../mixins";

/**
 * A simple wrapper over the input-tag, with styling that follows library conventions.
 */
export const Input = styled.input`
  border: 0;
  border-bottom: 1px solid ${(props) => props.theme.typeLibrary.color.outline.base};
  width: 100%;
  padding: ${(props) => props.theme.typeLibrary.spacing.small};
  padding-left: ${(props) => props.theme.typeLibrary.spacing.xxs};
  height: 40px;
  background-color: transparent;
  color: ${(props) => props.theme.typeLibrary.color.surface.on};
  border-radius: 0;

  ${getTextRole("body-large")};

  ::placeholder {
    color: ${(props) => props.theme.typeLibrary.color.outline.base};
  }
`;
