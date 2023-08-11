import styled from "styled-components/macro";

export const FullPageSpinnerContainer = styled.div`
  position: absolute;
  display: flex;
  align-items: center;
  justify-content: center;
  gap: ${(props) => props.theme.mimirorg.spacing.xxxl};
  background-color: ${(props) => props.theme.mimirorg.color.background.base};
  height: 100%;
  width: 100%;
`;
