import styled from "styled-components/macro";

export const FormContainer = styled.form`
  flex: 1;
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: min(${(props) => props.theme.mimirorg.spacing.multiple(14)}, 8vw);
  padding-left: min(${(props) => props.theme.mimirorg.spacing.multiple(11)}, 5vw);
  padding-right: min(${(props) => props.theme.mimirorg.spacing.multiple(11)}, 5vw);
  padding-top: ${(props) => props.theme.mimirorg.spacing.multiple(6)};
  min-width: 300px;
`;

export const FormBaseFieldsContainer = styled.fieldset`
  flex: 1;
  display: flex;
  flex-direction: column;
  flex-grow: 0;
  gap: ${(props) => props.theme.mimirorg.spacing.xl};
  border: 0;
  padding: 0;
  min-width: 300px;
`;
