import styled from "styled-components/macro";
import { Color } from "../colors";
import { FontSize, FontType, FontWeight } from "../font";

interface Props {
  flex?: string | number;
  height?: string;
  minHeight?: string;
  disabled?: boolean;
  hideOverflow?: boolean;
}

const ListWrapper = styled.div<Props>`
  display: flex;
  flex-direction: column;
  flex: ${(props) => (props.flex === undefined ? 1 : props.flex)};
  color: ${Color.Black};
  font-family: ${FontType.Standard};
  font-size: ${FontSize.Small};
  font-weight: ${FontWeight.Normal};
  opacity: ${(props) => (props.disabled ? 0.4 : 1)};
  height: ${(props) => (props.height === undefined ? 'auto' : props.height)};
  min-height: ${(props) => (props.minHeight === undefined ? 'auto' : props.minHeight)};
  overflow: ${(props) => (props.hideOverflow ? 'hidden' : 'revert')};
`;

export default ListWrapper;
