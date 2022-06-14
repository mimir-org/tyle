import styled from "styled-components/macro";
import { getTextRole } from "../../mixins";

export const Textarea = styled.textarea`
  border: 1px solid ${(props) => props.theme.tyle.color.outline.base};
  border-radius: ${(props) => props.theme.tyle.border.radius.small};
  min-height: 150px;
  padding: ${(props) => props.theme.tyle.spacing.xxs} ${(props) => props.theme.tyle.spacing.xs};
  color: ${(props) => props.theme.tyle.color.surface.on};

  ${getTextRole("body-large")};

  ::placeholder {
    color: ${(props) => props.theme.tyle.color.outline.base};
  }
`;
