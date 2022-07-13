import { css } from "styled-components/macro";

export interface BorderSystem {
  radius: {
    small: string,
    medium: string,
    large: string,
  }
}

export const border: BorderSystem = {
  radius: {
    small: "3px",
    medium: "5px",
    large: "10px",
  },
};

export const variablesBorder = css`
  :root {
    --tl-sys-border-radius-small: ${border.radius.small};
    --tl-sys-border-radius-medium: ${border.radius.medium};
    --tl-sys-border-radius-large: ${border.radius.large};
  }
`;


