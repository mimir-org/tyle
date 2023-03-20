import { colorReference } from "complib/core/variables/color/reference/colorReference";
import { ColorTheme } from "complib/core/variables/color/types/colorTheme";

export const darkTheme: ColorTheme = {
  primary: {
    base: colorReference.primary[60],
    on: colorReference.primary[99]
  },
  secondary: {
    base: colorReference.secondary[70],
    on: colorReference.secondary[0],
    container: {
      base: colorReference.secondary[20],
      on: colorReference.secondary[0]
    }
  },
  tertiary: {
    base: colorReference.tertiary[10],
    on: colorReference.primary[90],
    container: {
      base: colorReference.tertiary[30],
      on: colorReference.primary[0]
    }
  },
  error: {
    base: colorReference.error[80],
    on: colorReference.error[20]
  },
  warning: {
    base: colorReference.warning[90],
    on: colorReference.warning[10]
  },
  dangerousAction: {
    base: colorReference.dangerousAction[90],
    on: colorReference.dangerousAction[10]
  },
  outline: {
    base: colorReference.neutralVariant[40]
  },
  background: {
    base: colorReference.neutral[0],
    on: colorReference.neutral[90],
    inverse: {
      base: colorReference.neutral[99],
      on: colorReference.neutral[10]
    }
  },
  surface: {
    base: colorReference.neutral[10],
    on: colorReference.neutral[95],
    variant: {
      base: colorReference.neutralVariant[30],
      on: colorReference.neutralVariant[80]
    },
    inverse: {
      base: colorReference.neutral[99],
      on: colorReference.neutral[10]
    },
    tint: {
      base: colorReference.primary[80]
    }
  },
  shadow: {
    base: colorReference.neutral[0]
  },
  pure: {
    base: colorReference.neutral[0],
    on: colorReference.neutral[100]
  }
};