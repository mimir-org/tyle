import styled from "styled-components/macro";
import { Color } from "../../../../../compLibrary/colors";
import { FontSize } from "../../../../../compLibrary/font";

const DropdownMenuHeader = styled.div`
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
  height: 27px;
  background: ${Color.White};
  border: 1.5px solid ${Color.Black};
  border-radius: 5px;
  font-size: ${FontSize.Standard};
  color: ${Color.Black};
  padding: 5px 10px;
`;

export default DropdownMenuHeader;
