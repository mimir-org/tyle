import styled from "styled-components/macro";
import { Color } from "../../../../compLibrary/colors";
import { FontWeight } from "../../../../compLibrary/font";

interface Props {
  isSelected: boolean;
}

const RdsListElement = styled.div<Props>`
  display: flex;
  flex-direction: row;
  align-items: center;
  gap: 15px;
  height: 30px;
  padding: 2px 5px;

  &:hover {
    background-color: ${Color.BlueLight} !important;
  }
  
  label {
    text-decoration: ${(props) => props.isSelected && "underline"};
    font-weight: ${(props) => props.isSelected && FontWeight.Bold};
  }
`;

export default RdsListElement;
