import styled from "styled-components/macro";

export const UnauthenticatedContainer = styled.div`
  height: 100%;
  background: linear-gradient(
    250deg,
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
`;

export const UnauthenticatedContentContainer = styled.div`
  display: flex;
  justify-content: center;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.xxl};
  height: 100%;
  width: min(700px, 100%);
  padding: ${(props) => props.theme.tyle.spacing.xl} min(${(props) => props.theme.tyle.spacing.multiple(14)}, 10%);
  background-color: ${(props) => props.theme.tyle.color.sys.surface.base};
  box-shadow: ${(props) => props.theme.tyle.shadow.xl};
  overflow: auto;
`;
