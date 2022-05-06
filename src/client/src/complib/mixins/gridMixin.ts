import { css } from "styled-components/macro";
import { Grid } from "../props";

export const gridMixin = css<Grid>`
  gap: ${(props) => props.gap};
  column-gap: ${(props) => props.columnGap};
  row-gap: ${(props) => props.rowGap};
  grid-column: ${(props) => props.gridColumn};
  grid-row: ${(props) => props.gridRow};
  grid-auto-flow: ${(props) => props.gridAutoFlow};
  grid-auto-columns: ${(props) => props.gridAutoColumns};
  grid-auto-rows: ${(props) => props.gridAutoRows};
  grid-template-columns: ${(props) => props.gridTemplateColumns};
  grid-template-rows: ${(props) => props.gridTemplateRows};
  grid-template-areas: ${(props) => props.gridTemplateAreas};
  grid-area: ${(props) => props.gridArea};
`;
