import { colorReference } from "complib/core/variables/color/reference/colorReference";
import { ColorTheme } from "complib/core/variables/color/types/colorTheme";

export const darkTheme: ColorTheme = {
  primary: {
    base: colorReference.neutral[50],
    on: colorReference.primary[0]
  },
  secondary: {
    base: colorReference.secondary[70],
    on: colorReference.secondary[0],
    container: {
      base: colorReference.secondary[10],
      on: colorReference.secondary[0]
    }
  },
  tertiary: {
    base: colorReference.tertiary[20],
    on: colorReference.primary[99],
    container: {
      base: colorReference.tertiary[50],
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
    base: colorReference.dangerousAction[10],
    on: colorReference.dangerousAction[60]
  },
  outline: {
    base: colorReference.neutralVariant[30]
  },
  background: {
    base: colorReference.neutral[10],
    on: colorReference.neutral[90],
    inverse: {
      base: colorReference.neutral[90],
      on: colorReference.neutral[20]
    }
  },
  surface: {
    base: colorReference.neutral[20],
    on: colorReference.neutral[80],
    variant: {
      base: colorReference.neutralVariant[30],
      on: colorReference.neutralVariant[50]
    },
    inverse: {
      base: colorReference.neutral[90],
      on: colorReference.neutral[20]
    },
    tint: {
      base: colorReference.primary[80]
    }
  },
  shadow: {
    base: colorReference.neutral[50]
  },
  pure: {
    base: colorReference.neutral[20],
    on: colorReference.neutral[100]
  },
  badge: {
    success: {
      base: colorReference.secondary[80],
      on: colorReference.secondary[10],
    },
    error: {
      base: colorReference.error[40],
      on: colorReference.error[100],

    },
    warning: {
      base: colorReference.neutral[80],
      on: colorReference.neutral[10],
    },}
};