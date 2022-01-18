import styled from "styled-components/macro";

interface Props {
  height: number;
}

const ChooseProperties = styled.div<Props>`
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  padding: 0 20px;
  gap: 15px;
  position: relative;
  height: ${(props) => props.height}px;
`;

export default ChooseProperties;
