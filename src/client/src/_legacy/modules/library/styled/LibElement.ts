import styled from "styled-components/macro";
import { Color } from "../../../../compLibrary/colors";
import { FontSize } from "../../../../compLibrary/font";

interface Props {
  active?: boolean;
}

const LibElement = styled.button<Props>`
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 5px;
  width: calc(100% - 15px);
  height: 30px;
  border: 1px;
  border-color: ${(props) => (props.active ? Color.Black : Color.GreyDarker)};
  border-style: ${(props) => (props.active ? "dashed" : "revert")};
  border-radius: 3px;
  background-color: ${Color.White};
  margin: 5px 0 5px 15px;
  font-size: ${FontSize.Standard};
  padding-left: 10px;
  cursor: pointer;

  &:hover {
    background-color: ${Color.BlueLight};
    text-decoration: underline;
  }
`;

export default LibElement;
