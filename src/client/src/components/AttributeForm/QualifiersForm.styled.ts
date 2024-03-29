import styled from "styled-components/macro";

export const QualifiersFormWrapper = styled.form`
  display: flex;
  flex-wrap: wrap;
  max-width: 40rem;
  gap: ${(props) => props.theme.tyle.spacing.xl};

  & > * {
    flex: 1;
  }
`;
