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
  gap: var(--tl-sys-spacing-xs);
  min-height: 350px;
  min-width: min(500px, 100%);
  max-width: 100%;
  box-shadow: var(--tl-sys-shadow-box-medium);
  background-color: var(--tl-sys-color-surface);
  color: var(--tl-sys-color-on-surface-variant);
  border-radius: var(--tl-sys-border-radius-large);
  padding: var(--tl-sys-spacing-large);
`;

export const ModalHeader = styled.header`
  margin-bottom: var(--tl-sys-spacing-small);
`;

export const ModalHeaderTitle = styled.h1`
  font: var(--tl-sys-font-h2);
  color: var(--tl-sys-color-on-surface);
  margin: 0;
`;

export const ModalHeaderDescription = styled.p`
  font: var(--tl-sys-font-subtext);
  color: var(--tl-sys-color-on-surface-variant);
  margin: 0;
`;
