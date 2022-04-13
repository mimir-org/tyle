const BREAKPOINTS = {
  PHONE_MAX: 550,
  TABLET_MAX: 1100,
  LAPTOP_MAX: 1500,
};

/*
 * Queries for desktop first approach
 * Default size: Desktop monitors, 1501px and up
 */
export const QUERIES = {
  PHONE_AND_DOWN: `(max-width: ${BREAKPOINTS.PHONE_MAX}px)`,
  TABLET_AND_DOWN: `(max-width: ${BREAKPOINTS.TABLET_MAX}px)`,
  LAPTOP_AND_DOWN: `(max-width: ${BREAKPOINTS.LAPTOP_MAX}px)`,
};
