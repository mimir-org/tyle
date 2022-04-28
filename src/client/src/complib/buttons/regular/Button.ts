import styled from "styled-components/macro";

export const Button = styled.button`
  position: relative;
  display: inline-flex;
  justify-content: center;
  align-items: center;
  gap: var(--tl-sys-spacing-xs);
  white-space: nowrap;

  text-decoration: none;
  line-height: 1;

  color: var(--tl-sys-color-on-primary);
  background-color: var(--tl-sys-color-primary);

  border: 0;
  border-radius: var(--tl-sys-border-radius-medium);
  padding: var(--tl-sys-spacing-small) var(--tl-sys-spacing-small);

  :hover {
    cursor: pointer;
    background-color: var(--tl-sys-color-on-primary-container);
  }

  :active {
    background-color: var(--tl-sys-color-primary-container);
    color: var(--tl-sys-color-on-primary-container);
  }

  :disabled {
    cursor: not-allowed;
    box-shadow: none;
    opacity: 0.6;
  }
`;
