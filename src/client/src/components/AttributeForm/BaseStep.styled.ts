import styled from "styled-components/macro";

export const BaseStepWrapper = styled.form`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.mimirorg.spacing.xl};
  max-width: 35rem;
`;
