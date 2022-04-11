import styled from "styled-components/macro";
import { Color } from "../../../compLibrary/colors";

const SearchBarList = styled.div`
  display: flex;
  flex-direction: column;
  background-color: ${Color.White};
  border: 1px solid ${Color.BlueMagenta};
  border-radius: 5px;
  padding: 1px;
  font-size: 11px;
  position: absolute;
  top: 23px;
  left: 0;
  width: 100%;
  z-index: 1;
`;

export default SearchBarList;
