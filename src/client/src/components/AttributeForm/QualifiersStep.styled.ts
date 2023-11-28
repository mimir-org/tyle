import styled from "styled-components/macro";

export const QualifiersStepWrapper = styled.form`
  display: flex;
  flex-wrap: wrap;
  max-width: 40rem;
  gap: ${(props) => props.theme.mimirorg.spacing.xl};

  & > * {
    flex: 1;
  }
`;
