import styled from "styled-components/macro";

export const FieldsCardContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: ${(props) => props.theme.mimirorg.spacing.xl};
  padding: ${(props) => props.theme.mimirorg.spacing.xl};
  border-radius: ${(props) => props.theme.mimirorg.border.radius.large};
  background-color: ${(props) => props.theme.mimirorg.color.surface.base};
  width: fit-content;
`;
