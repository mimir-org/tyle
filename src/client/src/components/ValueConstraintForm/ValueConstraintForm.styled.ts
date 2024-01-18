import styled from "styled-components/macro";

export const ValueConstraintFormWrapper = styled.form`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.xxl};
  max-width: 50rem;
`;

export const ValueConstraintFormHeader = styled.div`
  display: flex;
  gap: ${(props) => props.theme.tyle.spacing.l};
  justify-content: space-between;
`;

export const ConstraintTypeSelectionWrapper = styled.div`
  display: flex;
  gap: ${(props) => props.theme.tyle.spacing.xl};

  & > * {
    flex: 1;
  }
`;
