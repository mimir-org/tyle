import { css } from "styled-components/macro";

export interface QuerySystem {
  breakpoints: {
    phoneMax: number,
    tabletMax: number,
    laptopMax: number,
  },
  phoneAndBelow: () => string,
  tabletAndBelow: () => string,
  laptopAndBelow: () => string,
}

export const queries: QuerySystem = {
  breakpoints: {
    phoneMax: 550,
    tabletMax: 1100,
    laptopMax: 1500,
  },
  phoneAndBelow: () => `(max-width: ${queries.breakpoints.phoneMax}px)`,
  tabletAndBelow: () => `(max-width: ${queries.breakpoints.tabletMax}px)`,
  laptopAndBelow: () => `(max-width: ${queries.breakpoints.laptopMax}px)`,
};

export const variablesQueries = css`
  :root {
    --tl-ref-breakpoint-phone-max: ${queries.breakpoints.phoneMax};
    --tl-ref-breakpoint-tablet-max: ${queries.breakpoints.tabletMax};
    --tl-ref-breakpoint-laptop-max: ${queries.breakpoints.laptopMax};
    --tl-sys-query-phone-below: ${queries.phoneAndBelow()};
    --tl-sys-query-tablet-below: ${queries.tabletAndBelow()};
    --tl-sys-query-laptop-below: ${queries.laptopAndBelow()};
  }
`;


