import styled from "styled-components/macro";

interface Props {
  size: number;
}

const Icon = styled.img<Props>`
  width: ${(props) => (props.size + 'px')};
  height: ${(props) => (props.size + 'px')};
`;

export default Icon;