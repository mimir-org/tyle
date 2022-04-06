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
