import styled from "styled-components/macro";
import { Color } from "../../../../compLibrary/colors";
import { FontSize, FontType } from "../../../../compLibrary/font";

interface Props {
  disabled?: boolean;
}

const ButtonsContainer = styled.div<Props>`
  display: flex;
  flex-direction: row;
  justify-content: space-between;

  button {
    min-width: 151px;
  height: 34px;
  margin-bottom: 0;
  display: flex;
  flex-direction: row;
  align-items: center;
  background: ${Color.White};
  border: 2px solid ${Color.BlueMagenta};
  border-radius: 5px;
  padding: 0 16px;
  white-space: nowrap;
  cursor: pointer;
  font-family: ${FontType.Standard};
  font-size: ${FontSize.Standard};
  color: ${Color.Black};
  opacity: ${(props) => (props.disabled ? 0.4 : 1)};

  .button-text {
    padding: 0 !important;
  }
`;

export default ButtonsContainer;
