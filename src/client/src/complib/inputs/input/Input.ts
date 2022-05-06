import styled from "styled-components";

/**
 * A simple wrapper over the input-tag, with styling that follows library conventions.
 */
export const Input = styled.input`
  border: 0;
  border-bottom: 1px solid ${(props) => props.theme.typeLibrary.color.outline.base};
  width: 100%;
  padding: ${(props) => props.theme.typeLibrary.spacing.small};
  height: 40px;
  background-color: transparent;
  color: ${(props) => props.theme.typeLibrary.color.surface.on};
  border-radius: 0;

  ::placeholder {
    color: ${(props) => props.theme.typeLibrary.color.surface.variant.on};
  }
`;
