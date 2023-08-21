import { focus } from "@mimirorg/component-library";
import styled from "styled-components/macro";

export const ItemDescriptionContainer = styled.button`
  border: 0;
  border-radius: ${(props) => props.theme.mimirorg.border.radius.medium};
  background-color: transparent;
  text-align: start;

  flex: 1;
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.mimirorg.spacing.s};
  color: ${(props) => props.theme.mimirorg.color.surface.on};

  margin-right: auto;

  min-width: 250px;
  max-width: 400px;
  height: 100px;

  :hover {
    cursor: pointer;
  }

  ${focus};
`;
