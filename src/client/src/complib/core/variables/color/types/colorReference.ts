import { Palette } from "./palette";

export interface ColorReference {
  primary: Palette;
  secondary: Palette;
  tertiary: Palette;
  error: Palette;
  neutral: Palette;
  neutralVariant: Palette;
}