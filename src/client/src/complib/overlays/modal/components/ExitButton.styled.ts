import styled from "styled-components";

export const ExitButtonContainer = styled.button`
  position: absolute;
  top: 0;
  right: 0;
  padding: ${(props) => props.theme.tyle.spacing.small};
  line-height: 0;
  background: transparent;
  border: 0;
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  cursor: pointer;
`;
