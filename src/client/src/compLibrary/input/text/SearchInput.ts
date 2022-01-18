import styled from "styled-components/macro";
import { Color } from "../../colors";
import { FontSize, FontWeight } from "../../font";
import { SearchIcon } from "../../../assets/icons/common";

const SearchInput = styled.input`
  height: 30px;
  width: 100%;
  padding: 0 5px;
  font-style: ${FontWeight.Italic};
  font-size: ${FontSize.Standard};
  border: 1px solid ${Color.White};
  border-radius: 3px;
  background-image: url(${SearchIcon});
  background-origin: content-box;
  background-position: right 6px;
  background-repeat: no-repeat;

  &:hover {
    background-color: ${Color.BlueLight};
  }

  &:focus {
    border-color: ${Color.BlueMagenta};
  }
`;

export default SearchInput;
