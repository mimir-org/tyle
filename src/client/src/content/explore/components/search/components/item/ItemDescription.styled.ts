import styled from "styled-components/macro";
import { focus } from "../../../../../../complib/mixins";

export const ItemDescriptionContainer = styled.button`
  border: 0;
  border-radius: ${(props) => props.theme.tyle.border.radius.medium};
  background-color: transparent;
  text-align: start;

  flex: 1;
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.s};
  color: ${(props) => props.theme.tyle.color.sys.surface.on};

  margin-right: auto;

  min-width: 250px;
  max-width: 400px;
  height: 100px;

  :hover {
    cursor: pointer;
  }

  ${focus};
`;
