import styled from "styled-components";
import { FontSize, FontWeight } from "../../../compLibrary/font";

export const ErrorBody = styled.div`
  max-height: 440px;
  overflow: auto;
`;

export const ErrorItem = styled.div`
  max-width: 400px;
  margin-top: 10px;
`;

export const ErrorItemText = styled.p`
  font-size: ${FontSize.Medium};
  margin: 5px 0 0;
`;

export const ErrorItemTitle = styled.h2`
  margin: 0;
  font-weight: ${FontWeight.Bold};
  font-size: ${FontSize.SubHeader};
`;
