import styled from "styled-components/macro";
import { Color } from "../../../../compLibrary/colors";

const ValuesListWrapper = styled.div`
  display: flex;
  flex-direction: column;
  margin: 0 15px 0 5px;
  border-width: 1px 1px 1px 1px;
  border-color: ${Color.Black};
  border-style: solid;
  border-radius: 5px;
  position: absolute;
  width: 307px;
  top: 28px;
  left: 27px;
  right: 0;
  z-index: 1;
  background-color: ${Color.White};
`;

export default ValuesListWrapper;
