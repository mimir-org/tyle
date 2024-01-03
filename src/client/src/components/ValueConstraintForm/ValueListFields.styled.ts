import styled from "styled-components/macro";

export const ValueListFieldsWrapper = styled.div`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.l};
`;

export const ValueListItemsWrapper = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  gap: ${(props) => props.theme.tyle.spacing.l};

  & > div:first-child {
    flex-grow: 1;
  }

  & > svg {
    color: ${(props) => props.theme.tyle.color.dangerousAction.on};
    cursor: pointer;
    width: 1.2rem;
    height: 1.2rem;
  }
`;

export const AddValueListItemWrapper = styled.div`
  display: flex;
  justify-content: center;

  & > svg {
    color: ${(props) => props.theme.tyle.color.primary.base};
    width: 2rem;
    height: 2rem;
    cursor: pointer;
  }
`;
