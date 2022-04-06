import styled from "styled-components";

export const UnauthenticatedFormContainer = styled.div`
  display: flex;
  justify-content: center;
  flex-direction: column;
  gap: var(--spacing-xxl);
  height: 100%;
  width: min(700px, 100%);
  padding: var(--spacing-medium) min(var(--spacing-xxxl), 10%);
  background-color: var(--color-background-primary);
  box-shadow: var(--shadow-box-xl);
  overflow: auto;
`;

export const UnauthenticatedFormButton = styled.button`
  min-height: 45px;
  border: 1px solid var(--color-border-primary);
  background-color: var(--color-info);
  color: var(--color-text-primary-inverted);
  border-radius: var(--border-radius-small);

  :active {
    border-width: 2px;
    background-color: var(--color-info-light);
    font-weight: var(--font-weight-bold);
  }

  :hover {
    background-color: var(--color-info-dark);
  }
`;
