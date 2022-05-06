import styled, {keyframes} from "styled-components/macro";

const AnimationName = keyframes`
  0%{background-position:50% 0}
  50%{background-position:50% 100%}
  100%{background-position:50% 0}
`;

export const HomeContainer = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 100%;
  height: 100%;

  background: linear-gradient(350deg, #ccf5ad, #88ddda);
  background-size: 400% 400%;
  animation: ${AnimationName} 8s ease infinite;
`;

export const HomeTitle = styled.h1``;
export const HomeText = styled.p``;



