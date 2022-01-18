import styled from "styled-components/macro";
import { Color } from "../colors";

const DropdownHeader = styled.div`
  display: flex;
  flex: 1;
  align-items: center;
  flex-direction: row;
  width: 63px;
  border: 1px solid ${Color.BlueMagenta};
  border-radius: 5px;
  padding: 0 5px;
  font-size: 11px;
  background-color: ${Color.White} !important;

  p {
    margin: 0;
  }

  .icon {
    margin-left: auto;
    width: 10px;
    height: 6px;
  }
`;

export default DropdownHeader;
