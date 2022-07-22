import styled from "styled-components/macro";

export const FullPageSpinnerContainer = styled.div`
  position: absolute;
  display: flex;
  align-items: center;
  justify-content: center;
  gap: ${(props) => props.theme.tyle.spacing.xxxl};
  background-color: ${(props) => props.theme.tyle.color.sys.background.base};
  height: 100%;
  width: 100%;
`;
