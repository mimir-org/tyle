import styled from "styled-components/macro";

export const SettingsContainer = styled.div`
  flex: 1;
  display: flex;
  flex-wrap: no-wrap;
  align-content: flex-start;
  gap: min(${(props) => props.theme.mimirorg.spacing.multiple(16)}, 8vw);
  padding-left: min(${(props) => props.theme.mimirorg.spacing.multiple(12)}, 6vw);
  padding-right: min(${(props) => props.theme.mimirorg.spacing.multiple(12)}, 6vw);
  padding-top: ${(props) => props.theme.mimirorg.spacing.multiple(6)};
  padding-bottom: ${(props) => props.theme.mimirorg.spacing.xl};

  @media ${(props) => props.theme.mimirorg.queries.tabletAndBelow} {
    flex-wrap: wrap;
  }
`;
