import styled from "styled-components/macro";

interface Props {
  flex?: string | number;
}

const TypePreviewColumn = styled.div<Props>`
  display: flex;
  flex: ${(props) => (props.flex === undefined ? 1 : props.flex)};
  flex-direction: column;
  max-height: inherit;
`;

export default TypePreviewColumn;
