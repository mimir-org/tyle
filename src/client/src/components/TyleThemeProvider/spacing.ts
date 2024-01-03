export interface SpacingSystem {
  unit: number;
  xs: string;
  s: string;
  base: string;
  l: string;
  xl: string;
  xxl: string;
  xxxl: string;
  multiple: (scalar: number) => string;
}

const spacingUnit = 8;

export const spacing: SpacingSystem = {
  unit: spacingUnit,
  xs: "2px",
  s: "4px",
  base: "8px",
  l: "12px",
  xl: "16px",
  xxl: "20px",
  xxxl: "24px",
  multiple: (multiplier: number) => `${multiplier * spacingUnit}px`,
};

export const getSpacingsOnly = () => {
  const partialSpacingSystem: Partial<SpacingSystem> = { ...spacing };
  delete partialSpacingSystem.unit;
  delete partialSpacingSystem.multiple;
  return partialSpacingSystem;
};
