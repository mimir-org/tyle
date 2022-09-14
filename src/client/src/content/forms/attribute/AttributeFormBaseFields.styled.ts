import styled from "styled-components/macro";

export const AttributeFormBaseFieldsContainer = styled.fieldset`
  flex: 1;
  display: flex;
  flex-direction: column;
  flex-grow: 0;
  align-items: center;
  gap: ${(props) => props.theme.tyle.spacing.xl};
  border: 0;
  padding: 0;
`;
