import { focus, getTextRole } from "@mimirorg/component-library";
import styled from "styled-components";

export const DigitsInputContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: ${(props) => props.theme.mimirorg.spacing.l};
`;

export const DigitsInput = styled.input`
  text-align: center;

  width: 60px;
  height: 60px;

  border: 1px solid ${(props) => props.theme.mimirorg.color.outline.base};
  border-radius: ${(props) => props.theme.mimirorg.border.radius.medium};

  background-color: ${(props) => props.theme.mimirorg.color.pure.base};
  color: ${(props) => props.theme.mimirorg.color.background.on};

  ${focus};
  ${getTextRole("headline-large")};

  ::placeholder {
    color: ${(props) => props.theme.mimirorg.color.outline.base};
  }
`;
