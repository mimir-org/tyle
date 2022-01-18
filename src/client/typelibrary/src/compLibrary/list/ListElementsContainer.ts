import styled from "styled-components/macro";
import { Color } from "../../compLibrary/colors";

interface Props {
  background: boolean;
  hover: boolean;
  switchBackground: boolean;
}

const ListElementsContainer = styled.div<Props>`
  position: relative;
  height: 100%;
  border-bottom: 1px solid ${Color.Black};
  overflow-x: hidden;
  overflow-y: auto;

  div:nth-child(odd) {
    background-color: ${(props) => props.background && Color.PurpleLight};
    background-color: ${(props) => props.switchBackground && Color.White};
  }

  div:nth-child(even) {
    background-color: ${(props) => props.background && Color.White};
    background-color: ${(props) => props.switchBackground && Color.PurpleLight};
  }

  div:nth-child(n):hover {
    background-color: ${(props) => !props.hover && Color.BlueLight};
  }

  :before {
    content: "";
    width: 1px;
    height: 5px;
    background: ${Color.BlueMagenta};
    border-radius: 1px;
    position: absolute;
    bottom: 0;
    left: 0;
  }

  :after {
    content: "";
    width: 1px;
    height: 5px;
    background: ${Color.BlueMagenta};
    border-radius: 1px;
    position: absolute;
    bottom: 0;
    right: 0;
  }
`;

export default ListElementsContainer;
