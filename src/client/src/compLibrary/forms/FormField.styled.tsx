import styled from "styled-components";

export const Container = styled.div`
  display: flex;
  flex-direction: column;
  gap: var(--spacing-small);
`;

export const Label = styled.label`
  font-weight: var(--font-weight-bold);
`;

export const Error = styled.p`
  color: var(--color-negative);
`;
