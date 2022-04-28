import styled, { keyframes } from "styled-components/macro";

const spin = keyframes`
  to { transform: rotate(360deg); }
`;

export const Spinner = styled.div`
  display: inline-block;
  width: 50px;
  height: 50px;
  border: 5px solid var(--tl-sys-color-inverse-surface);
  border-top-color: var(--tl-sys-color-inverse-on-surface);
  border-radius: 50%;
  animation: ${spin} 1s ease-in-out infinite;
`;
