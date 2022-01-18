import styled from "styled-components/macro";
import { Color } from "../../../../../compLibrary/colors";
import { FontSize } from "../../../../../compLibrary/font";

interface Props {
  disabled: boolean
}

const DropdownMenuWrapper = styled.div<Props>`
  display: flex;
  flex-direction: column;
  min-width: 15%;
  margin-right: 25px;
  background-color: ${Color.White};
  opacity: ${(props: { disabled: boolean }) => (props.disabled ? 0.4 : 1)};
  font-size: ${FontSize.Tiny};
  color: ${Color.Black};
  position: relative;

  .dropdown-label {
    margin-bottom: 7px;
  }
`;

export default DropdownMenuWrapper;
