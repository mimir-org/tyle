import styled from "styled-components";
import { getTextRole } from "../../mixins";

/**
 * A simple wrapper over the input-tag, with styling that follows library conventions.
 */
export const Input = styled.input`
  height: 40px;
  min-width: 250px;

  padding: ${(props) => props.theme.tyle.spacing.xs};
  border: 1px solid ${(props) => props.theme.tyle.color.outline.base};
  border-radius: ${(props) => props.theme.tyle.border.radius.small};

  ${getTextRole("body-large")};

  ::placeholder {
    color: ${(props) => props.theme.tyle.color.outline.base};
  }
`;
