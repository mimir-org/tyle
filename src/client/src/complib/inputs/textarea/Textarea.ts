import styled from "styled-components/macro";
import { getTextRole } from "../../mixins";

export const Textarea = styled.textarea`
  border: 1px solid ${(props) => props.theme.tyle.color.sys.outline.base};
  border-radius: ${(props) => props.theme.tyle.border.radius.medium};
  min-height: 150px;
  padding: ${(props) => props.theme.tyle.spacing.base};
  color: ${(props) => props.theme.tyle.color.sys.surface.on};

  ${getTextRole("body-large")};

  ::placeholder {
    color: ${(props) => props.theme.tyle.color.sys.outline.base};
  }
`;
