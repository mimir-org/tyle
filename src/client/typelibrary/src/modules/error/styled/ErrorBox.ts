import styled from "styled-components/macro";
import { Color } from "../../../compLibrary/colors";
import { FontSize } from "../../../compLibrary/font";

interface Props {
  visible: boolean;
}

const ErrorBox = styled.div<Props>`
  width: 400px;
  margin: auto;
  max-height: 440px;
  border: 2px solid ${Color.RedWarning};
  background-color: ${Color.White};
  box-shadow: 0 5px 6px rgba(0, 0, 0, 0.15);
  border-radius: 5px;
  font-weight: bold;
  font-size: ${FontSize.Header};
  position: absolute;
  top: 25%;
  bottom: 25%;
  left: 25%;
  right: 25%;
  padding: 20px 6px 30px 20px;
  visibility: ${(props) => !props.visible && "hidden"};
  overflow: hidden;
  z-index: 100;
`;

export default ErrorBox;
