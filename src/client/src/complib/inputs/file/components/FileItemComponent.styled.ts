import styled from "styled-components";

export const FileItemContainer = styled.div`
  margin-top: 1em;
  & .fileitem-delete {
    :hover {
      cursor: pointer;
      border: 2px solid black;
      border-radius: 50%;
    }
  }
`;

FileItemContainer.defaultProps = {};
