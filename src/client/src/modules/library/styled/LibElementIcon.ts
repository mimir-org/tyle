import styled from "styled-components/macro";
import { Color } from "../../../compLibrary/colors";

const LibElementIcon = styled.div`
  display: flex;
  width: 20px;
  max-width: 20px;
  height: 20px;
  border: 1.8px solid ${Color.Black};
  border-radius: 3px;
  background-color: ${(props: { color: string }) => props.color};

  .icon {
    width: 100%;
    margin: 0 auto;
  }
`;

export default LibElementIcon;
