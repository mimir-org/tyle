import styled from "styled-components/macro";
import { Color } from "../../colors";

interface Props {
  inputType?: string;
  readOnly?: boolean;
  fontSize?: string;
  fontStyle?: string;
}

const Input = styled.input<Props>`
  border: 1px solid ${Color.Black};
  width: 100%;
  box-sizing: border-box;
  border-radius: 5px;
  padding: 10px;
  height: 30px;
  text-align: left;
  margin-right: ${(props) => props.inputType === "tech" && "4px"};
  background-color: ${(props) => (props.readOnly ? Color.GreyLight : Color.White)};
  font-size: ${(props) => GetFontSize(props.fontSize)};
  font-style: ${(props) => props.fontStyle ? props.fontStyle : 'revert'};

  @media (min-width: 3000px) {
    height: 40px;
    font-size: 16px;
  }
`;

const GetFontSize = (fontSize: string) => fontSize ?? "13px";
export default Input;
