import { css } from "styled-components/macro";

export const variablesShadow = css`
  :root {
    --shadow-box-small: 0 4px 8px -1px hsla(0, 0%, 0%, 0.2);
    --shadow-box-medium: 0 6px 20px -2px hsla(0, 0%, 0%, 0.2);
    --shadow-box-large: 0 15px 50px -10px hsla(0, 0%, 0%, 0.3);
    --shadow-box-xl: 0 25px 80px -15px hsla(0, 0%, 0%, 0.5);
  }
`;

export const SHADOW = {
  BOX_SMALL: "var(--shadow-box-small)",
  BOX_MEDIUM: "var(--shadow-box-medium)",
  BOX_LARGE: "var(--shadow-box-large)",
};
