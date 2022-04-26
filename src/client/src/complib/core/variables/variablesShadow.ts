import { css } from "styled-components/macro";

export const variablesShadow = css`
  :root {
    --tl-sys-shadow-box-small: 0 4px 8px -1px hsla(0, 0%, 0%, 0.2);
    --tl-sys-shadow-box-medium: 0 6px 20px -2px hsla(0, 0%, 0%, 0.2);
    --tl-sys-shadow-box-large: 0 15px 50px -10px hsla(0, 0%, 0%, 0.3);
    --tl-sys-shadow-box-xl: 0 25px 80px -15px hsla(0, 0%, 0%, 0.5);
  }
`;

export const shadow = {
  boxSmall: "var(--tl-sys-shadow-box-small)",
  boxMedium: "var(--tl-sys-shadow-box-medium)",
  boxLarge: "var(--tl-sys-shadow-box-large)",
  boxXL: "var(--tl-sys-shadow-box-xl)",
};
