import styled from "styled-components/macro";

export const ConnectTerminalsWrapper = styled.div`
  display: flex;
  gap: ${(props) => props.theme.mimirorg.spacing.multiple(6)};
`;

export const SymbolPreview = styled.div`
  width: 100%;
  max-width: 300px;
  margin: 0 auto;
  position: relative;
`;

export const RemoveSymbolIconWrapper = styled.div`
  width: fit-content;
  position: absolute;
  top: -1rem;
  right: -1rem;
  cursor: pointer;

  & > svg {
    color: ${(props) => props.theme.mimirorg.color.dangerousAction.on};
  }
`;

export const ConnectionPointList = styled.div`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.mimirorg.spacing.base};
  margin: 0 auto;
`;

export const ConnectionPointListItem = styled.div`
  display: flex;
  gap: ${(props) => props.theme.mimirorg.spacing.xl};
  align-items: center;

  & p {
    font: ${(props) => props.theme.mimirorg.typography.roles.body.large.font};
  }
`;
