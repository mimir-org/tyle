const BREAKPOINTS = {
  phoneMax: 550,
  tabletMax: 1100,
  laptopMax: 1500,
};

/*
 * Queries for desktop first approach
 * Default size: Desktop monitors, 1501px and up
 */
export const QUERIES = {
  phoneAndDown: `(max-width: ${BREAKPOINTS.phoneMax}px)`,
  tabletAndDown: `(max-width: ${BREAKPOINTS.tabletMax}px)`,
  laptopAndDown: `(max-width: ${BREAKPOINTS.laptopMax}px)`,
};
