import styled from "styled-components/macro";

export const ExploreContainer = styled.div`
  flex: 1;
  display: flex;
  flex-wrap: wrap;
  gap: min(${(props) => props.theme.tyle.spacing.multiple(27)}, 12vw);
  padding-left: min(${(props) => props.theme.tyle.spacing.multiple(12)}, 6vw);
  padding-right: min(${(props) => props.theme.tyle.spacing.multiple(12)}, 6vw);
  padding-top: ${(props) => props.theme.tyle.spacing.multiple(6)};
  padding-bottom: ${(props) => props.theme.tyle.spacing.xl};
  overflow: auto;
`;
