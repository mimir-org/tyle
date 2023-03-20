import { Accent } from "complib/core/variables/color/types/accent";

export interface ColorTheme {
  primary: Accent,
  secondary: Accent,
  tertiary: Accent,
  error: Accent,
  warning: Accent,
  dangerousAction: Accent,
  outline: Pick<Accent, "base">,
  background: Pick<Accent, "base" | "on"> & {
    inverse: Pick<Accent, "base" | "on">
  },
  surface: Pick<Accent, "base" | "on"> & {
    variant: Pick<Accent, "base" | "on">,
    inverse: Pick<Accent, "base" | "on">
    tint: Pick<Accent, "base">
  }
  shadow: Pick<Accent, "base">,
  pure: Accent,
}