import styled from "styled-components/macro";

export const AuthContentContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  flex-direction: column;
  justify-content: space-between;
  gap: ${(props) => props.theme.tyle.spacing.multiple(4)};

  width: 550px;
  min-height: 600px;

  padding: min(${(props) => props.theme.tyle.spacing.multiple(6)}, 5vw);
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  background-color: ${(props) => props.theme.tyle.color.surface.base};
`;

export const AuthContentSection = styled.div`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.xxxl};
`;
