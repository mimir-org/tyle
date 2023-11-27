import styled from "styled-components/macro";

export const TypeFormContainerWrapper = styled.div`
  padding: ${(props) => props.theme.mimirorg.spacing.multiple(6)}
    min(${(props) => props.theme.mimirorg.spacing.multiple(11)}, 5vw);
`;

export const TypeFormContainerHeader = styled.h1`
  margin-bottom: ${(props) => props.theme.mimirorg.spacing.multiple(6)};
`;

export const TypeFormContainerBody = styled.div`
  display: flex;
  gap: ${(props) => props.theme.mimirorg.spacing.multiple(18)};
`;
