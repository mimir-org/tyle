import styled from "styled-components/macro";

export const RangeFieldsWrapper = styled.div`
  display: flex;
  gap: ${(props) => props.theme.mimirorg.spacing.xl};

  & > * {
    flex: 1;
  }
`;
