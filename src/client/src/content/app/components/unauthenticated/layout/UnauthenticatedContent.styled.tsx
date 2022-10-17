import styled from "styled-components/macro";

export const UnauthenticatedContentContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  align-items: baseline;

  height: fit-content;
  width: fit-content;
`;

export const UnauthenticatedContentPrimaryContainer = styled.div`
  position: relative;
  flex: 1 0 350px;
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.xxxl};

  max-width: 550px;
  min-height: 650px;
  padding: ${(props) => props.theme.tyle.spacing.multiple(6)};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};

  background-color: ${(props) => props.theme.tyle.color.sys.surface.base};
`;

export const UnauthenticatedContentSecondaryContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.base};

  max-width: min(85%, 50ch);
  min-height: 250px;

  padding: ${(props) => props.theme.tyle.spacing.xxxl};

  background-color: ${(props) => props.theme.tyle.color.sys.tertiary.container?.base};
  color: ${(props) => props.theme.tyle.color.sys.tertiary.container?.on};
`;
