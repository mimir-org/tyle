import { css } from "styled-components/macro";

export const variablesBorder = css`
  :root {
    --border-radius-small: 4px;
    --border-radius-medium: 8px;
    --border-radius-large: 16px;
  }
`;

export const BORDER = {
  RADIUS: {
    SMALL: "var(--border-radius-small)",
    MEDIUM: "var(--border-radius-medium)",
    LARGE: "var(--border-radius-large)",
  },
};
