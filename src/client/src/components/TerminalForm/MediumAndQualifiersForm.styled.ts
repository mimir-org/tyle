import styled from "styled-components/macro";

export const MediumAndQualifierFormWrapper = styled.form`
  display: flex;
  gap: ${(props) => props.theme.mimirorg.spacing.xl};
  max-width: 40rem;
`;

export const MediumSelectWrapper = styled.div`
  flex: 1;
`;

export const QualifierSelectWrapper = styled.div`
  flex: 1;
`;