import styled from "styled-components";

/**
 * A simple wrapper around fieldset to control padding/margins/spacing around form inputs
 */
export const FormFieldset = styled.fieldset`
  display: flex;
  flex-direction: column;
  gap: var(--spacing-medium);

  border: 0;
  padding: 0;
`;
