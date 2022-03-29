import styled from "styled-components";

/**
 * A simple wrapper over the input-tag, with styling that follows library conventions.
 */
const Input = styled.input`
  border: 1px solid var(--color-neutral-dark);
  width: 100%;
  border-radius: 5px;
  padding: 10px;
  height: 30px;
`;

export default Input;
