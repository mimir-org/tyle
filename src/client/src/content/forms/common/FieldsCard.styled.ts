import styled from "styled-components/macro";

export const FieldsCardContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: ${(props) => props.theme.tyle.spacing.xl};
  padding: ${(props) => props.theme.tyle.spacing.xl};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  background-color: ${(props) => props.theme.tyle.color.sys.surface.base};
  width: fit-content;
`;
