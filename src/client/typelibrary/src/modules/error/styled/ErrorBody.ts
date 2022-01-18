import styled from "styled-components/macro";
import { FontSize, FontWeight } from "../../../compLibrary/font";

const ErrorBody = styled.div`
  max-height: 440px;
  font-weight: ${FontWeight.Bold};
  font-size: ${FontSize.Header};
  overflow: auto;
  z-index: 100;
`;

export default ErrorBody;
