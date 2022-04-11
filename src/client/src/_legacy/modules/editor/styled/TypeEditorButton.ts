import styled from "styled-components/macro";
import { Color } from "../../../../compLibrary/colors";
import { FontSize } from "../../../../compLibrary/font";

interface Props {
  disabled: boolean;
}

const TypeEditorButton = styled.button<Props>`
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: space-evenly;
  height: 30px;
  padding: 0 10px;
  border: 1px solid ${Color.BlueMagenta};
  border-radius: 3px;
  background-color: ${(props) => (props.disabled ? Color.Grey : Color.White)};
  cursor: ${(props) => (props.disabled ? "not-allowed" : "pointer")};
  color: ${Color.Black};
  font-size: ${FontSize.Standard};
  white-space: nowrap;
`;

export default TypeEditorButton;
