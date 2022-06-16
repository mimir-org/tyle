import styled from "styled-components";

export const UnauthenticatedFormContainer = styled.div`
  display: flex;
  justify-content: center;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.xxl};
  height: 100%;
  width: min(700px, 100%);
  padding: ${(props) => props.theme.tyle.spacing.xl} min(${(props) => props.theme.tyle.spacing.multiple(14)}, 10%);
  background-color: ${(props) => props.theme.tyle.color.sys.surface.base};
  box-shadow: ${(props) => props.theme.tyle.shadow.xl};
  overflow: auto;
`;
