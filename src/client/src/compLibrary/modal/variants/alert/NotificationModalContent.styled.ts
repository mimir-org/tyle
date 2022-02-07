import styled from "styled-components";
import { Color } from "../../../colors";
import { Icon } from "../../../icon";

interface NotificationModalContentContainerProps {
  isWarning: boolean;
}

export const NotificationModalContentContainer = styled.div<NotificationModalContentContainerProps>`
  position: relative;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 15px;
  min-width: 260px;
  min-height: 180px;
  border: 2px solid ${(props) => (props.isWarning ? Color.RedWarning : Color.Black)};
  border-radius: 5px;
  background-color: ${Color.White};
  box-shadow: 0 5px 5px rgba(0, 0, 0, 0.15);
  padding: 40px 25px 15px 25px;
`;

export const NotificationModalDescription = styled.p`
  width: 200px;
  text-align: center;
  margin: 0 0 auto;
`;

export const NotificationWarningIcon = styled(Icon)`
  position: absolute;
  top: 10px;
  left: 10px;
`;
