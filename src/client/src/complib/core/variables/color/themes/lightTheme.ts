import { colorReference } from "../reference/colorReference";
import { ColorTheme } from "../types/colorTheme";

export const lightTheme: ColorTheme = {
  primary: {
    base: colorReference.primary[10],
    on: colorReference.primary[100],
  },
  secondary: {
    base: colorReference.secondary[80],
    on: colorReference.secondary[0],
  },
  tertiary: {
    base: colorReference.tertiary[80],
    on: colorReference.primary[10],
    container: {
      base: colorReference.tertiary[95],
      on: colorReference.primary[10]
    }
  },
  error: {
    base: colorReference.error[40],
    on: colorReference.error[100],
  },
  outline: {
    base: colorReference.neutralVariant[50]
  },
  background: {
    base: colorReference.neutral[99],
    on: colorReference.neutral[10]
  },
  surface: {
    base: colorReference.neutral[99],
    on: colorReference.neutral[10],
    variant: {
      base: colorReference.neutralVariant[90],
      on: colorReference.neutralVariant[30],
    },
    inverse: {
      base: colorReference.neutral[10],
      on: colorReference.neutral[99],
    },
    tint: {
      base: colorReference.primary[10]
    }
  },
  shadow: {
    base: colorReference.neutral[0],
  }
};