import styled from "styled-components/macro";

export const NodeTerminalContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.xl};
  width: fit-content;
  max-width: 760px;
  padding: ${(props) => props.theme.tyle.spacing.xl};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  background-color: ${(props) => props.theme.tyle.color.sys.surface.base};
  border: 1px solid ${(props) => props.theme.tyle.color.sys.tertiary.base};
`;

export const NodeTerminalInputContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: ${(props) => props.theme.tyle.spacing.xxxl};
`;
