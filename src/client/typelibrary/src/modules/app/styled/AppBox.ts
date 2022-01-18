import styled from "styled-components/macro";
import { Color } from "../../../compLibrary/colors";

interface Props {
  fetching: boolean;
}

const AppBox = styled.div<Props>`
  width: 100%;
  height: 100%;
  background: ${(props) => props.fetching && Color.Grey};
  opacity: ${(props) => props.fetching && 0.2};
`;

export default AppBox;
