import { Palette } from "complib/core/variables/color/types/palette";

export interface ColorReference {
  primary: Palette;
  secondary: Palette;
  tertiary: Palette;
  error: Palette;
  warning: Palette;
  neutral: Palette;
  neutralVariant: Palette;
  dangerousAction: Palette;
}