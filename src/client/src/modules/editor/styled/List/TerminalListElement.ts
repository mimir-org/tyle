import styled from "styled-components/macro";
import { Color } from "../../../../compLibrary/colors";
import { FontSize, FontType } from "../../../../compLibrary/font";

const TerminalListElement = styled.div`
  display: flex;
  flex-direction: column;
  width: inherit;
  padding: 2px 1px;
  font-family: ${FontType.Standard};
  font-size: ${FontSize.Small};
  color: ${Color.Black};

  :nth-child(odd) {
    background-color: ${Color.PurpleLight};
  }
  :nth-child(even) {
    background-color: ${Color.White};
  }

  p {
    margin: 0;
  }

  Input::placeholder,
  Input {
    background-color: ${Color.White};
    color: ${Color.Black};
    font-family: ${FontType.Standard};
    font-size: ${FontSize.Standard};
  }
`;

export default TerminalListElement;
