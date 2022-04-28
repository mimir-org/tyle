import styled, { keyframes } from "styled-components/macro";

const spin = keyframes`
  to { transform: rotate(360deg); }
`;

export const Spinner = styled.div`
  display: inline-block;
  width: 50px;
  height: 50px;
  border: 5px solid ${(props) => props.theme.typeLibrary.color.surface.inverse.base};
  border-top-color: ${(props) => props.theme.typeLibrary.color.surface.inverse.on};
  border-radius: 50%;
  animation: ${spin} 1s ease-in-out infinite;
`;
