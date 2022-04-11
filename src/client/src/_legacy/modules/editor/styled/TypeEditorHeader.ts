import styled from "styled-components/macro";
import { Color } from "../../../../compLibrary/colors";
import { FontSize } from "../../../../compLibrary/font";

const TypeEditorHeader = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: space-between;

  p {
    margin: 13px 0 0 20px;
    font-weight: bold;
    font-size: ${FontSize.Header};
    color: ${Color.Black};
  }

  img {
    margin: 18px 16px 0 0;
    cursor: pointer;
  }
`;

export default TypeEditorHeader;
