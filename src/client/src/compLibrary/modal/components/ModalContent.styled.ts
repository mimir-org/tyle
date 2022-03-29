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
  gap: 5px;
  min-height: 350px;
  min-width: min(500px, 100%);
  max-width: 100%;
  border: 2px solid var(--color-ui-primary);
  background-color: var(--color-neutral-light);
  box-shadow: 0 5px 5px rgba(0, 0, 0, 0.15);
  border-radius: 5px;
  padding: 30px;
`;

export const ModalHeader = styled.header`
  margin-bottom: 10px;
`;

export const ModalHeaderTitle = styled.h1`
  font-weight: bold;
  font-size: var(--font-size-header);
  margin: 0;
`;

export const ModalHeaderDescription = styled.p`
  margin: 0;
`;
