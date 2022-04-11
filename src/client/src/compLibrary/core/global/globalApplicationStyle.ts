import { css } from "styled-components/macro";

/**
 * APPLICATION SPECIFIC GLOBALS
 */
export const globalApplicationStyle = css`
  html,
  body {
    font-family: var(--font-family-primary);
  }

  ::-webkit-scrollbar {
    width: 14px;
    height: 18px;
  }

  ::-webkit-scrollbar-thumb {
    border: 4px solid transparent;
    background-clip: padding-box;
    border-radius: 7px;
    background-color: var(--color-grey-scale-3);
  }

  ::-webkit-scrollbar-track {
    background-color: transparent;
  }
`;
