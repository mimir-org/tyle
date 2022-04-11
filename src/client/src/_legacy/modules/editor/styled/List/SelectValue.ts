import styled from "styled-components/macro";
import { Color } from "../../../../../compLibrary/colors";

interface Props {
  isSelected: boolean;
}

const SelectValue = styled.div<Props>`
  display: flex;
  flex-direction: column;
  position: relative;
  border-top: ${(props) => (props.isSelected ? "dashed 1px" + Color.GreyDark : 0)};
`;

export default SelectValue;
