import { css } from "styled-components/macro";

export const variablesBorder = css`
  :root {
    --tl-sys-border-radius-small: 4px;
    --tl-sys-border-radius-medium: 8px;
    --tl-sys-border-radius-large: 16px;
  }
`;

export const border = {
  radius: {
    small: "var(--tl-sys-border-radius-small)",
    medium: "var(--tl-sys-border-radius-medium)",
    large: "var(--tl-sys-border-radius-large)",
  },
};
