import { Theme } from "components/TyleThemeProvider/theme";

declare module "styled-components" {
  export interface DefaultTheme {
    tyle: Theme;
  }
}
