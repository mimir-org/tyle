import styled from "styled-components/macro";

export const ValueConstraintStepWrapper = styled.form`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.mimirorg.spacing.xxl};
  max-width: 50rem;
`;

export const ValueConstraintStepHeader = styled.div`
  display: flex;
  gap: ${(props) => props.theme.mimirorg.spacing.l};
  justify-content: space-between;
`;

export const ConstraintTypeSelectionWrapper = styled.div`
  display: flex;
  gap: ${(props) => props.theme.mimirorg.spacing.xl};

  & > * {
    flex: 1;
  }
`;

export const RangeFieldsWrapper = styled.div`
  display: flex;
  gap: ${(props) => props.theme.mimirorg.spacing.xl};

  & > * {
    flex: 1;
  }
`;
