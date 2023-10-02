import styled from "styled-components/macro";

export const UnauthenticatedLayoutContainer = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh;
  padding: ${(props) => props.theme.mimirorg.spacing.base};
  overflow: auto;
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
