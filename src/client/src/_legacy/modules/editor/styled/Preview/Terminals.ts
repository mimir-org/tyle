import styled from "styled-components/macro";
interface Props {
  input?: boolean;
}

const Terminals = styled.div<Props>`
  max-height: 130px;
  display: flex;
  flex-direction: column;
  position: absolute;
  top: 5px;
  left: ${(props) => props.input && '-120px'};
  right: ${(props) => !props.input && '-120px'};
  span {
    padding-bottom: 2px;
  }
`;

export default Terminals;
