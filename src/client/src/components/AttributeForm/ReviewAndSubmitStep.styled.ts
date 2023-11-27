import styled from "styled-components/macro";

export const ReviewAndSubmitStepWrapper = styled.div`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.mimirorg.spacing.xl};
`;

export const SubmitButtonsWrapper = styled.div`
  display: flex;
  gap: ${(props) => props.theme.mimirorg.spacing.xl};
`;
