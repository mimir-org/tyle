export interface Accent {
  base: string;
  on: string;
  container?: Accent;
}

export interface Palette {
  0: string;
  10: string;
  20: string;
  30: string;
  40: string;
  50: string;
  60: string;
  70: string;
  80: string;
  90: string;
  95: string;
  99: string;
  100: string;
}

export interface ColorReference {
  primary: Palette;
  secondary: Palette;
  tertiary: Palette;
  success: Palette;
  error: Palette;
  warning: Palette;
  neutral: Palette;
  neutralVariant: Palette;
  dangerousAction: Palette;
  functionAspect: Palette;
  productAspect: Palette;
  locationAspect: Palette;
}

export interface ColorSystem {
  reference: ColorReference;
  text: Accent;
  primary: Accent;
  secondary: Accent;
  tertiary: Accent;
  success: Accent;
  error: Accent;
  warning: Accent;
  outline: Pick<Accent, "base">;
  background: Pick<Accent, "base" | "on"> & {
    inverse: Pick<Accent, "base" | "on">;
  };
  surface: Pick<Accent, "base" | "on"> & {
    variant: Pick<Accent, "base" | "on">;
    inverse: Pick<Accent, "base" | "on">;
    tint: Pick<Accent, "base">;
  };
  dangerousAction: Accent;
  shadow: Pick<Accent, "base">;
  pure: Accent;
  functionAspect: Accent;
  productAspect: Accent;
  locationAspect: Accent;
  badge: {
    success: Accent;
    error: Accent;
    warning: Accent;
    info: Accent;
  };
}

export const colorReference: ColorReference = {
  primary: {
    0: "#000000",
    10: "#3d113f",
    20: "#561750",
    30: "#641c59",
    40: "#722261",
    50: "#7c2666",
    60: "#90407a",
    70: "#a35e8f",
    80: "#be8aae",
    90: "#d8b8ce",
    95: "#efe2eb",
    99: "#f6eff5",
    100: "#ffffff",
  },
  secondary: {
    0: "#000000",
    10: "#39862d",
    20: "#5eaa3e",
    30: "#72be47",
    40: "#88d452",
    50: "#98e55b",
    60: "#a7ea72",
    70: "#b7f08b",
    80: "#ccf5ac",
    90: "#e0f9cd",
    95: "#f3fdeb",
    99: "#f3ffec",
    100: "#ffffff",
  },
  tertiary: {
    0: "#000000",
    10: "#001945",
    20: "#002d6f",
    30: "#16448f",
    40: "#345ca8",
    50: "#4f75c3",
    60: "#6a8fdf",
    70: "#85aafc",
    80: "#adc6ff",
    90: "#d7e2ff",
    95: "#ecf0ff",
    99: "#fdfbff",
    100: "#ffffff",
  },
  success: {
    0: "#000000",
    10: "#002106",
    20: "#00390f",
    30: "#005319",
    40: "#006e24",
    50: "#008a30",
    60: "#18a740",
    70: "#2cb34a",
    80: "#5fe070",
    90: "#7cfd89",
    95: "#c7ffc3",
    99: "#f6fff0",
    100: "#ffffff",
  },
  error: {
    0: "#000000",
    10: "#410001",
    20: "#680003",
    30: "#930006",
    40: "#ba1b1b",
    50: "#dd3730",
    60: "#ff5449",
    70: "#ff897a",
    80: "#ffb4a9",
    90: "#ffdad4",
    95: "#ffede9",
    99: "#fcfcfc",
    100: "#ffffff",
  },
  warning: {
    0: "#000000",
    10: "#221B00",
    20: "#3B2F00",
    30: "#554500",
    40: "#715D00",
    50: "#8D7500",
    60: "#AB8E12",
    70: "#C7A930",
    80: "#E4C44A",
    90: "#FFE175",
    95: "#FFF3D4",
    99: "#FFFBFF",
    100: "#ffffff",
  },
  dangerousAction: {
    0: "#000000",
    10: "#A20021",
    20: "#b13236",
    30: "#be4e4c",
    40: "#cb6963",
    50: "#d7827b",
    60: "#e19a94",
    70: "#eab3ae",
    80: "#f2ccc8",
    90: "#f5d5d2",
    95: "#f9e6e3",
    99: "#fbeae8",
    100: "#ffffff",
  },
  neutral: {
    0: "#000000",
    10: "#13131c",
    20: "#32333c",
    30: "#50515b",
    40: "#64646f",
    50: "#8b8b97",
    60: "#acacb8",
    70: "#d0d0dd",
    80: "#e2e2ef",
    90: "#eeeefa",
    95: "#f7f6ff",
    99: "#fdfbff",
    100: "#ffffff",
  },
  neutralVariant: {
    0: "#000000",
    10: "#2d2d2d",
    20: "#4f4f4f",
    30: "#6f6f6f",
    40: "#848484",
    50: "#aeaeae",
    60: "#cbcbcb",
    70: "#ededed",
    80: "#f2f2f2",
    90: "#f7f7f7",
    95: "#fcfcfc",
    99: "#fefefe",
    100: "#ffffff",
  },
  functionAspect: {
    0: "#000000",
    10: "#1c1d00",
    20: "#502400",
    30: "#484a00",
    40: "#606200",
    50: "#797c06",
    60: "#939627",
    70: "#aeb140",
    80: "#FBC913", // Sunglow
    90: "#FFDE7A", // Jasmine
    95: "#FEF445", // Lemon Yellow
    99: "#FFFAA9", // Lemon Yellow Crayola
    100: "#ffffff",
  },
  productAspect: {
    0: "#000000",
    10: "#002022",
    20: "#00363a",
    30: "#004f54",
    40: "#006970",
    50: "#00848d",
    60: "#069098", // Viridian Green
    70: "#00bdc9",
    80: "#47DDE6", // Dark Turquoise
    90: "#00F0FF", // Electric Blue
    95: "#B9F5F9", // Celeste
    99: "#f4feff",
    100: "#ffffff",
  },
  locationAspect: {
    0: "#000000",
    10: "#0c0664",
    20: "#252478",
    30: "#A300A7", // Purple Munsell
    40: "#5455a9",
    50: "#6d6ec4",
    60: "#FA00FF", // Magenta
    70: "#F083F1", // Pink
    80: "#c1c1ff",
    90: "#FDCCFE", // Pink Lace
    95: "#f2efff",
    99: "#fffbff",
    100: "#ffffff",
  },
};

export const dark: ColorSystem = {
  reference: colorReference,
  text: {
    base: colorReference.neutralVariant[100],
    on: colorReference.neutralVariant[0],
  },
  primary: {
    base: colorReference.neutral[60],
    on: colorReference.primary[0],
  },
  secondary: {
    base: colorReference.secondary[70],
    on: colorReference.secondary[0],
    container: {
      base: colorReference.secondary[10],
      on: colorReference.secondary[0],
    },
  },
  tertiary: {
    base: colorReference.tertiary[20],
    on: colorReference.primary[99],
    container: {
      base: colorReference.tertiary[50],
      on: colorReference.primary[0],
    },
  },
  success: {
    base: colorReference.success[80],
    on: colorReference.success[20],
  },
  error: {
    base: colorReference.error[80],
    on: colorReference.error[20],
  },
  warning: {
    base: colorReference.warning[90],
    on: colorReference.warning[10],
  },
  dangerousAction: {
    base: colorReference.dangerousAction[10],
    on: colorReference.dangerousAction[60],
  },
  outline: {
    base: colorReference.neutralVariant[30],
  },
  background: {
    base: colorReference.neutral[10],
    on: colorReference.neutral[90],
    inverse: {
      base: colorReference.neutral[90],
      on: colorReference.neutral[20],
    },
  },
  surface: {
    base: colorReference.neutral[20],
    on: colorReference.neutral[80],
    variant: {
      base: colorReference.neutralVariant[30],
      on: colorReference.neutralVariant[50],
    },
    inverse: {
      base: colorReference.neutral[90],
      on: colorReference.neutral[20],
    },
    tint: {
      base: colorReference.primary[80],
    },
  },
  shadow: {
    base: colorReference.neutral[50],
  },
  pure: {
    base: colorReference.neutral[20],
    on: colorReference.neutral[100],
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
    },
    info: {
      base: colorReference.neutral[80],
      on: colorReference.neutral[10],
    },
  },
  functionAspect: {
    base: colorReference.functionAspect[95],
    on: colorReference.functionAspect[0],
  },
  productAspect: {
    base: colorReference.productAspect[90],
    on: colorReference.productAspect[0],
  },
  locationAspect: {
    base: colorReference.locationAspect[60],
    on: colorReference.locationAspect[0],
  },
};

export const light: ColorSystem = {
  reference: colorReference,
  text: {
    base: colorReference.neutralVariant[10],
    on: colorReference.neutralVariant[100],
  },
  primary: {
    base: colorReference.primary[10],
    on: colorReference.primary[100],
  },
  secondary: {
    base: colorReference.secondary[80],
    on: colorReference.secondary[0],
    container: {
      base: colorReference.secondary[99],
      on: colorReference.secondary[0],
    },
  },
  tertiary: {
    base: colorReference.tertiary[80],
    on: colorReference.primary[10],
    container: {
      base: colorReference.tertiary[95],
      on: colorReference.primary[10],
    },
  },
  success: {
    base: colorReference.success[70],
    on: colorReference.success[100],
  },
  error: {
    base: colorReference.error[40],
    on: colorReference.error[100],
  },
  warning: {
    base: colorReference.warning[95],
    on: colorReference.warning[0],
  },
  dangerousAction: {
    base: colorReference.dangerousAction[95],
    on: colorReference.dangerousAction[10],
  },
  outline: {
    base: colorReference.neutralVariant[60],
  },
  background: {
    base: colorReference.neutral[99],
    on: colorReference.neutral[10],
    inverse: {
      base: colorReference.neutral[10],
      on: colorReference.neutral[99],
    },
  },
  surface: {
    base: colorReference.neutral[95],
    on: colorReference.neutral[10],
    variant: {
      base: colorReference.neutralVariant[90],
      on: colorReference.neutralVariant[30],
    },
    inverse: {
      base: colorReference.neutral[10],
      on: colorReference.neutral[95],
    },
    tint: {
      base: colorReference.primary[10],
    },
  },
  shadow: {
    base: colorReference.neutral[0],
  },
  pure: {
    base: colorReference.neutral[100],
    on: colorReference.neutral[0],
  },
  badge: {
    success: {
      base: colorReference.secondary[90],
      on: colorReference.secondary[10],
    },
    error: {
      base: colorReference.error[40],
      on: colorReference.error[100],
    },
    warning: {
      base: colorReference.warning[90],
      on: colorReference.warning[30],
    },
    info: {
      base: colorReference.tertiary[90],
      on: colorReference.tertiary[10],
    },
  },
  functionAspect: {
    base: colorReference.functionAspect[95],
    on: colorReference.functionAspect[0],
  },
  productAspect: {
    base: colorReference.productAspect[90],
    on: colorReference.productAspect[0],
  },
  locationAspect: {
    base: colorReference.locationAspect[60],
    on: colorReference.locationAspect[0],
  },
};
