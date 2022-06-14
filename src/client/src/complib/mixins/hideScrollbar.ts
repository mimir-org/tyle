import { css } from "styled-components/macro";

/**
 * Hides the scrollbar
 *
 * When using this mixin you should make sure that the UI offers enough scroll affordance,
 * so that the end user is able to discover that the content is scrollable.
 */
export const hideScrollbar = css`
  scrollbar-width: none;
  -ms-overflow-style: none;
  ::-webkit-scrollbar {
    width: 0;
    height: 0;
  }
`;
