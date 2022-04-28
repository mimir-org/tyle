import styled from "styled-components";

export const UnauthenticatedFormContainer = styled.div`
  display: flex;
  justify-content: center;
  flex-direction: column;
  gap: var(--tl-sys-spacing-xxl);
  height: 100%;
  width: min(700px, 100%);
  padding: var(--tl-sys-spacing-medium) min(var(--tl-sys-spacing-xxxl), 10%);
  background-color: var(--tl-sys-color-surface);
  box-shadow: var(--tl-sys-shadow-box-xl);
  overflow: auto;
`;
