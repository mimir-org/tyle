import styled from "styled-components/macro";

interface Props {
  gap?: string
}

const LabelWrapper = styled.label<Props>`
  display: flex;
  gap: ${(props) => (props.gap ? props.gap : '10px')};

  &:hover {
    text-decoration: underline;
    cursor: pointer;
  }
`;

export default LabelWrapper;