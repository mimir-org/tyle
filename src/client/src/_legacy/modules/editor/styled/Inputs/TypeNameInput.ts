import styled from "styled-components/macro";
import { Color } from "../../../../../compLibrary/colors";
import { FontSize, FontType } from "../../../../../compLibrary/font";

interface Props {
  disabled?: boolean;
}

const TypeNameInput = styled.div<Props>`
  display: flex;
  flex-direction: column;
  min-width: 15%;
  margin-right: 25px;
  color: ${Color.Black};
  font-size: ${FontSize.Tiny};
  font-family: ${FontType.Standard};
  opacity: ${(props) => (props.disabled ? 0.4 : 1)};

  p {
    margin: 0;
  }

  .label {
    margin-bottom: 7px;
  }

  input::placeholder {
    color: ${Color.Black};
    font-size: ${FontSize.Standard};
    font-family: ${FontType.Standard};
  }

  input {
    max-height: 27px;
    padding: 5px 10px
  }
`;

export default TypeNameInput;
