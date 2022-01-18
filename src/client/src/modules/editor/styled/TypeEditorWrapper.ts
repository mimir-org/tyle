import styled from "styled-components/macro";
import { Size } from "../../../compLibrary/size";
import { Color } from "../../../compLibrary/colors";

const TypeEditorWrapper = styled.div`
  display: flex;
  flex-direction: column;
  position: fixed;
  top: ${Size.TopMenu_Height}px;
  left: calc(1.6px + ${Size.ModuleClosed}px);
  width: 95%;
  height: calc(100% - ${Size.TopMenu_Height}px - ${Size.ModuleClosed}px);
  background: ${Color.White};
  border: 2px solid ${Color.BlueMagenta};
  border-radius: 5px;
  z-index: 100;
`;

export default TypeEditorWrapper;
