import styled from "styled-components";

export const UnauthenticatedFormContainer = styled.div`
  display: flex;
  justify-content: center;
  flex-direction: column;
  gap: ${(props) => props.theme.typeLibrary.spacing.xxl};
  height: 100%;
  width: min(700px, 100%);
  padding: ${(props) => props.theme.typeLibrary.spacing.medium}
    min(${(props) => props.theme.typeLibrary.spacing.xxxl}, 10%);
  background-color: ${(props) => props.theme.typeLibrary.color.surface.base};
  box-shadow: ${(props) => props.theme.typeLibrary.shadow.boxXL};
  overflow: auto;
`;
