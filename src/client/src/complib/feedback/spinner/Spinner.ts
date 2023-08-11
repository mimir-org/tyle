import styled, { keyframes } from "styled-components/macro";

const rotate = keyframes`
  50% {
    border-radius: 30%;
    filter: hue-rotate(330deg) invert(150%);
    transform: scale(0.5) rotate(360deg);
  }
  100% {
    transform: scale(1) rotate(720deg);
  }
`;

interface SpinnerProps {
  size?: number;
}

export const Spinner = styled.div<SpinnerProps>`
  position: relative;

  &:before,
  &:after {
    content: "";
    position: relative;
    display: block;
  }

  &:before {
    width: ${(props) => props.size}px;
    height: ${(props) => props.size}px;
    border-radius: 50%;
    border: 2px solid ${(props) => props.theme.mimirorg.color.surface.inverse.base};
    animation: ${rotate} 2.5s cubic-bezier(0.75, 0, 0.5, 1) infinite normal;
    background: radial-gradient(
      circle,
      hsl(240deg 19% 9%) 0%,
      hsl(279deg 27% 12%) 10%,
      hsl(294deg 34% 14%) 20%,
      hsl(301deg 41% 16%) 30%,
      hsl(304deg 50% 19%) 40%,
      hsl(306deg 58% 21%) 50%,
      hsl(308deg 39% 30%) 60%,
      hsl(311deg 30% 38%) 70%,
      hsl(313deg 23% 47%) 80%,
      hsl(316deg 24% 55%) 90%,
      hsl(318deg 29% 64%) 100%
    );
  }
`;

Spinner.defaultProps = {
  size: 70,
};
