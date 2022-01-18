import styled from "styled-components/macro";
import { Color } from "../colors";
import { FontSize, FontType, FontWeight } from "../font";

interface Props {
  removeBorderBottom?: boolean;
  preview?: boolean;
}

const ListLabel = styled.div<Props>`
  font-family: ${FontType.Standard};
  font-size: ${FontSize.Standard};
  font-weight: ${FontWeight.Bold};
  color: ${Color.BlueMagenta};
  padding-bottom: ${(props) => (props.removeBorderBottom ? 0 : 8)}px;
  border-width: 0;
  border-bottom-width: ${(props) => (props.removeBorderBottom || props.preview ? 0 : 2)}px;
  border-style: solid;
  border-color: ${Color.BlueMagenta};
`;

export default ListLabel;
