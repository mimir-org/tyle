import styled from "styled-components/macro";

export const UnauthenticatedContentContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  flex-direction: column;
  justify-content: space-between;
  gap: ${(props) => props.theme.tyle.spacing.multiple(4)};

  width: 550px;
  min-height: 600px;

  padding: ${(props) => props.theme.tyle.spacing.multiple(6)};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  background-color: ${(props) => props.theme.tyle.color.sys.surface.base};
`;

export const UnauthenticatedContentSection = styled.div`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.xxxl};
`;
