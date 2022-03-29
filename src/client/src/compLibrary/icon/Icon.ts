import styled from "styled-components";

interface Props {
  size?: number;
}

/**
 * A simple wrapper over the img-tag
 * Has a default width and height of 15px
 * @param size sets height and width of icon
 */
export const Icon = styled.img<Props>`
  width: ${(props) => props.size + "px"};
  height: ${(props) => props.size + "px"};
`;

Icon.defaultProps = {
  size: 15,
};
