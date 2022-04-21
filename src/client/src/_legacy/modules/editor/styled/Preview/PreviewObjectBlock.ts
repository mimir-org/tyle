import styled from "styled-components/macro";

interface Props {
  minHeight: number;
  color: string;
}

const PreviewObjectBlock = styled.div<Props>`
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 200px;
  min-height: ${(props) => props.minHeight}px;
  background-color: ${(props) => props.color};
  border-radius: 10px;
  box-shadow: 0 5px 5px -2px rgb(0 0 0 / 20%);
`;

export default PreviewObjectBlock;
