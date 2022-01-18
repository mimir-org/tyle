import styled from "styled-components/macro";
import { Color } from "../colors";

const SearchBarListItem = styled.div`
  height: 18px;
  border-bottom: 1px solid ${Color.GreyDark};
  background-color: ${Color.White};

  p {
    padding: 3px 13px;
  }

  &:hover {
    background-color: ${Color.BlueLight};
    text-decoration: underline;
    cursor: pointer;
  }
`;

export default SearchBarListItem;
