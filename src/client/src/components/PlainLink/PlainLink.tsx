import { sizingMixin, Sizing } from "@mimirorg/component-library";
import { Link } from "react-router-dom";
import styled from "styled-components/macro";

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