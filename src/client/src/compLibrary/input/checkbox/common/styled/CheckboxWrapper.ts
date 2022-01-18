import styled from "styled-components/macro";

interface Props {
  color: string;
  marginLeft: number;
}

const CheckboxWrapper = styled.label<Props>`
  display: flex;
  align-items: center;
  cursor: pointer;
  height: 15px;
  width: 15px;
  background: transparent;
  margin-left: ${(props) => props.marginLeft}px;

  input {
    position: absolute;
    display: none;
  }

  & > svg {
    height: inherit;
    width: inherit;
    fill: ${(props) => props.color};
  }
`;

export default CheckboxWrapper;
