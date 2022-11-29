import styled from "styled-components/macro";

export const SettingsContainer = styled.div`
  flex: 1;
  display: flex;
  flex-wrap: wrap;
  align-content: flex-start;
  gap: min(${(props) => props.theme.tyle.spacing.multiple(16)}, 8vw);
  padding-left: min(${(props) => props.theme.tyle.spacing.multiple(12)}, 6vw);
  padding-right: min(${(props) => props.theme.tyle.spacing.multiple(12)}, 6vw);
  padding-top: ${(props) => props.theme.tyle.spacing.multiple(6)};
  padding-bottom: ${(props) => props.theme.tyle.spacing.xl};
`;
