import styled from "styled-components/macro";

export const AttributeFormContainer = styled.form`
  flex: 1;
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: min(${(props) => props.theme.tyle.spacing.multiple(14)}, 8vw);
  padding-left: min(${(props) => props.theme.tyle.spacing.multiple(11)}, 5vw);
  padding-right: min(${(props) => props.theme.tyle.spacing.multiple(11)}, 5vw);
  padding-top: ${(props) => props.theme.tyle.spacing.multiple(6)};
`;
