import styled from "styled-components";

export const ModalContainer = styled.div`
  position: fixed;
  inset: 0;
  overflow-y: auto;
`;

export const ModalLayoutContainer = styled.div`
  position: relative;
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh;
`;

interface ModalOverlayProps {
  isBlurred?: boolean;
  isFaded?: boolean;
}

export const ModalOverlay = styled.div<ModalOverlayProps>`
  position: fixed;
  inset: 0;
  backdrop-filter: ${(props) => props.isBlurred && "blur(5px)"};
  background-color: ${(props) => props.isFaded && props.theme.typeLibrary.color.surface.inverse.base};
  opacity: ${(props) => props.isFaded && 0.3};
`;
