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

export const HasSpecificBooleanValueFieldset = styled.fieldset`
  border: 0;
  display: flex;
  max-width: 15rem;
  justify-content: space-between;

  & > legend {
    color: ${(props) => props.theme.mimirorg.color.surface.variant.on};
    font-size: 0.75rem;
    font-weight: 600;
    letter-spacing: ${1 / 24}rem;
    line-height: 1rem;
  }
`;

export const ValueListFieldsWrapper = styled.div`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.mimirorg.spacing.l};
`;

export const ValueListItemsWrapper = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  gap: ${(props) => props.theme.mimirorg.spacing.l};

  & > div:first-child {
    flex-grow: 1;
  }

  & > svg {
    color: ${(props) => props.theme.mimirorg.color.dangerousAction.on};
    cursor: pointer;
    width: 1.2rem;
    height: 1.2rem;
  }
`;

export const AddValueListItemWrapper = styled.div`
  display: flex;
  justify-content: center;

  & > svg {
    color: ${(props) => props.theme.mimirorg.color.primary.base};
    width: 2rem;
    height: 2rem;
    cursor: pointer;
  }
`;

export const RangeFieldsWrapper = styled.div`
  display: flex;
  gap: ${(props) => props.theme.mimirorg.spacing.xl};

  & > * {
    flex: 1;
  }
`;
