import { Link } from "react-router-dom";
import styled from "styled-components/macro";

/**
 * Removes styles from react router links.
 * Useful when wrapping other elements with navigation semantics.
 */
export const PlainLink = styled(Link)`
  color: inherit;
  text-decoration: inherit;

  :link,
  :hover {
    color: inherit;
    text-decoration: inherit;
  }
`;
