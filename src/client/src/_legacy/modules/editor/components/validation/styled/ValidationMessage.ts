import { Color } from "../../../../../../compLibrary/colors";
import { FontSize } from "../../../../../../compLibrary/font";
import styled from "styled-components/macro";

const ValidationMessage = styled.span`
  margin-top: 5px;
  color: ${Color.RedWarning};
  font-size: ${FontSize.Standard};

  :empty:before {
    content: "\\200b";
  }
}
`;

export default ValidationMessage;
