import styled from "styled-components";

interface ModalContentContainerProps {
  title?: string;
  description?: string;
  color?: string;
}

export const ModalContentContainer = styled.div<ModalContentContainerProps>`
  position: relative;
  display: flex;
  flex-direction: column;
  gap: var(--spacing-small);
  min-height: 350px;
  min-width: min(500px, 100%);
  max-width: 100%;
  border: 2px solid var(--color-border-primary);
  background-color: var(--color-background-primary);
  box-shadow: var(--shadow-box-medium);
  border-radius: var(--border-radius-small);
  padding: var(--spacing-xl);
`;

export const ModalHeader = styled.header`
  margin-bottom: var(--spacing-small);
`;

export const ModalHeaderTitle = styled.h1`
  font: var(--font-h2);
  margin: 0;
`;

export const ModalHeaderDescription = styled.p`
  font: var(--font-subtext);
  margin: 0;
`;
