import { focus, getTextRole } from "@mimirorg/component-library";
import styled from "styled-components";

export const DigitsInputContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: ${(props) => props.theme.tyle.spacing.l};
`;

export const DigitsInput = styled.input`
  text-align: center;

  width: 60px;
  height: 60px;

  border: 1px solid ${(props) => props.theme.tyle.color.outline.base};
  border-radius: ${(props) => props.theme.tyle.border.radius.medium};

  background-color: ${(props) => props.theme.tyle.color.pure.base};
  color: ${(props) => props.theme.tyle.color.background.on};

  ${focus};
  ${getTextRole("headline-large")};

  ::placeholder {
    color: ${(props) => props.theme.tyle.color.outline.base};
  }
`;
