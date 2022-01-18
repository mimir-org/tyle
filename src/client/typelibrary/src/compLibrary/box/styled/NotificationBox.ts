import styled from "styled-components/macro";
import { Color } from "../../colors";
import { FontSize } from "../../font";

interface Props {
  color: string;
}

const NotificationBox = styled.div<Props>`
  display: flex;
  flex-direction: column;
  position: absolute;
  margin: auto;
  top: 25%;
  bottom: 25%;
  left: 25%;
  right: 25%;
  width: 285px;
  height: 172px;
  border: 1.5px solid ${(props) => props.color};
  border-radius: 10px;
  z-index: 10;
  background-color: ${Color.White};
  box-shadow: 0 5px 5px -2px rgba(0, 0, 0, 0.2);

  > p {
    display: flex;
    align-items: center;
    position: relative;
    height: 85px;
    top: 20px;
    margin: 10px 10px 0 10px;
    width: 265px;
    text-align: center;
    line-height: 1.5;
    font-size: ${FontSize.Standard};
  }
`;

export default NotificationBox;
