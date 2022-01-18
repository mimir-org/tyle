import styled from "styled-components/macro";

interface Props {
  visible: boolean;
}

const WarningBox = styled.div<Props>`
  display: flex;
  position: absolute;
  left: 3px;
  top: 3px;
  margin: 5px 0 0 8px;
  width: 16px;
  height: 16px;
  opacity: ${(props) => (props.visible ? 1 : 0)};
`;

export default WarningBox;
