import { Link } from "react-router-dom";
import { sizingMixin } from "styleConstants";
import styled from "styled-components/macro";
import { Sizing } from "types/styleProps";

type PlainLinkProps = Sizing;

/**
 * Removes styles from react router links.
 * Useful when wrapping other elements with navigation semantics.
 */
const PlainLink = styled(Link)<PlainLinkProps>`
  color: inherit;
  text-decoration: inherit;

  :link,
  :hover {
    color: inherit;
    text-decoration: inherit;
  }

  ${sizingMixin};
`;

export default PlainLink;
