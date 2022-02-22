import styled from "styled-components/macro";
import { Color } from "../../../../../compLibrary/colors";
import { FontSize } from "../../../../../compLibrary/font";

const TextInput = styled.input`
  border: 1.5px solid ${Color.Black};
  border-radius: 5px;
  background: ${Color.White};
  font-size: ${FontSize.Standard};
  color: ${Color.Black};
`;

export default TextInput;
