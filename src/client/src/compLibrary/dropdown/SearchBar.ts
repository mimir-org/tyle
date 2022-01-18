import styled from "styled-components/macro";
import { Color } from "../colors";
import { FontSize } from "../font";

const SearchBar = styled.div`
  display: flex;
  align-items: center;
  height: 100%;
  border: 1px solid ${Color.BlueMagenta};
  border-radius: 5px;
  background-color: ${Color.White} !important;
  padding: 0 5px 0 10px;
  input:focus,
  textarea:focus,
  select:focus {
    outline: none;
  }

  input[type="text"] {
    width: 100%;
    height: 12px;
    font-size: ${FontSize.Medium};
    border: 0;
  }

  input[type="text"]::placeholder {
    color: ${Color.GreyDarker};
    font-size: ${FontSize.Medium};
    font-style: italic;
    opacity: 0.5;
  }

  .icon {
    width: 10px;
    height: 6px;
  }
`;

export default SearchBar;
