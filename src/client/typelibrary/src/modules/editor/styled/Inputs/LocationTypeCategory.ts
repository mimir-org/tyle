import styled from "styled-components/macro";
import { Color } from "../../../../compLibrary/colors";
import { FontSize } from "../../../../compLibrary/font";

const LocationTypeCategory = styled.div`
  display: flex;
  height: 31px;
  align-items: center;
  border-width: 1px 0 1px 0;
  border-style: solid;
  border-color: ${Color.Grey};
  font-size: ${FontSize.Standard};
  font-weight: bold;
  color: ${Color.Black};
  background-color: ${Color.White};
  z-index: 1;

  p {
    text-align: left;
    padding: 5px;
  }
`;

export default LocationTypeCategory;
