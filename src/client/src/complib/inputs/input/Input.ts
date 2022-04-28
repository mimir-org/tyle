import styled from "styled-components";

/**
 * A simple wrapper over the input-tag, with styling that follows library conventions.
 */
export const Input = styled.input`
  border: 0;
  border-bottom: 1px solid var(--tl-sys-color-outline);
  width: 100%;
  padding: var(--tl-sys-spacing-small);
  height: 40px;
  background-color: transparent;
  color: var(--tl-sys-color-on-surface);
  border-radius: 0;

  ::placeholder {
    color: var(--tl-sys-color-on-surface-variant);
  }
`;
