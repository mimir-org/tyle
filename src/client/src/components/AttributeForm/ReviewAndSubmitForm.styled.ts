import styled from "styled-components/macro";

export const ReviewAndSubmitFormWrapper = styled.form`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.xl};
`;

export const SubmitButtonsWrapper = styled.div`
  display: flex;
  gap: ${(props) => props.theme.tyle.spacing.xl};
`;
