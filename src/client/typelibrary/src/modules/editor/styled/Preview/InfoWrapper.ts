import styled from "styled-components/macro";

interface Props {
  height?: string;
}

const InfoWrapper = styled.div<Props>`
  padding: 8px;
  height: ${(props) => (props.height ? props.height : 'auto')};
  max-width: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-evenly;
  gap: 16px;
`;

export default InfoWrapper;
