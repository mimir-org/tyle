import styled from "styled-components";

interface Props {
  size?: number;
}

/**
 * A simple wrapper over the img-tag
 * Has a default width and height of 1em
 * @param size sets height and width of icon
 */
export const Icon = styled.img<Props>`
  display: inline-block;
  width: ${(props) => (props.size ? `${props.size}px` : "1em")};
  height: ${(props) => (props.size ? `${props.size}px` : "1em")};
  line-height: 1;
`;
