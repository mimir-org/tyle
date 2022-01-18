import styled from "styled-components/macro";

interface Props {
  fetching: boolean;
}

const SpinnerWrapper = styled.div<Props>`
  width: 100px;
  height: 100px;
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);
  display: ${(props) => !props.fetching && "none"};
  z-index: 100;
`;

export default SpinnerWrapper;
