import styled from "styled-components";

/**
 * A simple wrapper around form to control general form layout
 */
export const Form = styled.form`
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  gap: ${(props) => props.theme.typeLibrary.spacing.xl};
  width: 100%;
`;
