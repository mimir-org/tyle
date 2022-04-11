import styled from "styled-components/macro";

interface ValidationContainerProps {
  flex?: string | number;
  minWidth?: string;
  maxWidth?: string;
}

const ValidationContainer = styled.div<ValidationContainerProps>`
  display: flex;
  flex-direction: column;
  flex: ${(props) => (props.flex ? props.flex : 'revert')};
  min-width: ${(props) => (props.minWidth ? props.minWidth : 'revert')};
  max-width: ${(props) => (props.maxWidth ? props.maxWidth : 'revert')};
`;

export default ValidationContainer;