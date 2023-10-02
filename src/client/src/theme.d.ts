import { Theme } from "@mimirorg/component-library";

declare module "styled-components" {
  export interface DefaultTheme {
    mimirorg: Theme;
  }
}
