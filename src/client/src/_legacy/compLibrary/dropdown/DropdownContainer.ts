import styled from "styled-components/macro";
import { Color } from "../../../compLibrary/colors";

const DropdownContainer = styled.div`
  display: flex;
  flex-direction: column;
  color: ${Color.Black};
  position: relative;
  max-height: 20px;
`;

export default DropdownContainer;
