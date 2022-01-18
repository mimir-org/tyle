import styled from "styled-components/macro";
import { Color } from "../../../compLibrary/colors";
import { FontSize } from "../../../compLibrary/font";

const LibCategoryButton = styled.button`
  display: inline-flex;
  align-items: center;
  justify-content: space-between;
  width: 100%;
  height: 30px;
  border: 1px solid ${Color.BlueMagenta};
  border-radius: 3px;
  background-color: ${Color.White};
  margin-top: 10px;
  font-size: ${FontSize.SubHeader};
  padding-left: 10px;
  padding-right: 14px;
  -webkit-user-select: none;
  -ms-user-select: none;
  user-select: none;

  .expandIcon {
    position: relative;
    left: 3px;
  }
`;

export default LibCategoryButton;
