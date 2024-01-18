import styled from "styled-components/macro";

export const BlockBaseFormWrapper = styled.form`
  display: grid;
  grid-template-columns: 1fr 1fr 1fr 1fr;
  grid-template-areas: "name name name notation" "aspect aspect purpose purpose" "description description description description";
  gap: ${(props) => props.theme.tyle.spacing.xl};
  max-width: 40rem;
`;

export const NameInputWrapper = styled.div`
  grid-area: name;
`;

export const NotationInputWrapper = styled.div`
  grid-area: notation;
`;

export const AspectSelectWrapper = styled.div`
  grid-area: aspect;
`;

export const PurposeSelectWrapper = styled.div`
  grid-area: purpose;
`;

export const DescriptionInputWrapper = styled.div`
  grid-area: description;
`;
