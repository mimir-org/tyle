import styled from "styled-components";

/**
 * A simple wrapper over the input-tag, with styling that follows library conventions.
 */
const Input = styled.input`
  border: 1px solid var(--color-border-primary);
  width: 100%;
  border-radius: var(--border-radius-small);
  padding: var(--spacing-small);
  height: 30px;
`;

export default Input;
