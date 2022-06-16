import { colorReference } from "../reference/colorReference";
import { ColorTheme } from "../types/colorTheme";

export const darkTheme: ColorTheme = {
  primary: {
    base: colorReference.primary[80],
    on: colorReference.primary[20],
  },
  secondary: {
    base: colorReference.secondary[80],
    on: colorReference.secondary[20],
  },
  tertiary: {
    base: colorReference.tertiary[80],
    on: colorReference.primary[20],
    container: {
      base: colorReference.tertiary[30],
      on: colorReference.primary[90]
    }
  },
  error: {
    base: colorReference.error[80],
    on: colorReference.error[20],
  },
  outline: {
    base: colorReference.neutralVariant[60]
  },
  background: {
    base: colorReference.neutral[10],
    on: colorReference.neutral[99]
  },
  surface: {
    base: colorReference.neutral[10],
    on: colorReference.neutral[99],
    variant: {
      base: colorReference.neutralVariant[30],
      on: colorReference.neutralVariant[80],
    },
    inverse: {
      base: colorReference.neutral[99],
      on: colorReference.neutral[10],
    },
    tint: {
      base: colorReference.primary[80]
    }
  },
  shadow: {
    base: colorReference.neutral[0],
  }
};