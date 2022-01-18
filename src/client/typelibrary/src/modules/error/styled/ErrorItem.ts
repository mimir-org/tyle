import styled from "styled-components/macro";
import { FontSize } from "../../../compLibrary/font";

const ErrorItem = styled.div`
  h3 {
    font-size: ${FontSize.SubHeader};
    margin-bottom: 5px;
  }
  p {
    font-size: ${FontSize.Medium};
    margin-top: 0;
  }
`;

export default ErrorItem;
