import styled from "styled-components/macro";

export const Button = styled.button`
  position: relative;
  display: inline-flex;
  justify-content: center;
  white-space: nowrap;

  text-decoration: none;
  line-height: 1;

  color: var(--color-text-primary-inverted);
  background-color: var(--color-info);
  border: 1px solid var(--color-info-dark);

  border-radius: var(--border-radius-small);
  padding: var(--spacing-small) var(--spacing-small);

  :hover {
    background-color: var(--color-info-dark);
  }

  :active {
    background-color: var(--color-info-light);
  }

  :disabled {
    cursor: not-allowed;
    box-shadow: none;
    opacity: 0.6;
  }
`;
