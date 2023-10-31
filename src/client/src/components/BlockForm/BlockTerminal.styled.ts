import styled from "styled-components/macro";

export const BlockTerminalContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.mimirorg.spacing.xl};
  width: fit-content;
  max-width: 760px;
  padding: ${(props) => props.theme.mimirorg.spacing.xl};
  border-radius: ${(props) => props.theme.mimirorg.border.radius.large};
  background-color: ${(props) => props.theme.mimirorg.color.surface.base};
  border: 1px solid ${(props) => props.theme.mimirorg.color.tertiary.base};
`;

export const BlockTerminalInputContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: ${(props) => props.theme.mimirorg.spacing.xxxl};
`;
