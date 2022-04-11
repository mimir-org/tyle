import styled from "styled-components/macro";
import { FontSize, FontWeight } from "../../../../compLibrary/font";

const LibCategoryHeader = styled.span`
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  cursor: pointer;
  font-size: ${FontSize.Standard};
  font-weight: ${FontWeight.Bold};
`;

export default LibCategoryHeader;
