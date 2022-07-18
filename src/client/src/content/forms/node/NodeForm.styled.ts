import styled, { css } from "styled-components/macro";

export const NodeFormContainer = styled.form`
  flex: 1;
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: ${(props) => css`min(${props.theme.tyle.spacing.multiple(14)}, 8vw)`};
  padding-left: ${(props) => css`min(${props.theme.tyle.spacing.multiple(11)}, 5vw)`};
  padding-right: ${(props) => css`min(${props.theme.tyle.spacing.multiple(11)}, 5vw)`};
  padding-top: ${(props) => props.theme.tyle.spacing.multiple(6)};
`;
