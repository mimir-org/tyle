import { math } from "polished";

export interface TypefaceReference {
  brand: string;
  weights: {
    bold: number;
    medium: number;
    normal: number;
    light: number;
  };
}

export interface TypeScaleDimension<T> {
  base: T;
  n3: T;
  n2: T;
  n1: T;
  p1: T;
  p2: T;
  p3: T;
  p4: T;
  p5: T;
  p6: T;
  p7: T;
}

export interface TypeScaleSpecification<T> {
  size: TypeScaleDimension<T>;
  lineHeight: TypeScaleDimension<T>;
}

export interface ScaleSpecification {
  font: string;
  size: string | number;
  weight: string | number;
  family: string;
  tracking: number;
  letterSpacing: string;
  lineHeight: string | number;
}

export interface NominalScale {
  large: ScaleSpecification;
  medium: ScaleSpecification;
  small: ScaleSpecification;
}

export interface TypographyRoles {
  display: NominalScale;
  headline: NominalScale;
  title: NominalScale;
  body: NominalScale;
  label: NominalScale;
}

export interface TypographySystem {
  typeface: TypefaceReference;
  typeScale: TypeScaleSpecification<number>;
  typeScaleSystem: TypeScaleSpecification<string>;
  roles: TypographyRoles;
}

const typefaceReference: TypefaceReference = {
  brand: "'Roboto', sans-serif",
  weights: {
    bold: 700,
    medium: 600,
    normal: 400,
    light: 300,
  },
};

const typeScale: TypeScaleSpecification<number> = {
  size: {
    base: 16,
    n3: 11,
    n2: 12,
    n1: 14,
    p1: 22,
    p2: 24,
    p3: 28,
    p4: 32,
    p5: 36,
    p6: 45,
    p7: 57,
  },
  lineHeight: {
    base: 24,
    n3: 10,
    n2: 16,
    n1: 20,
    p1: 28,
    p2: 32,
    p3: 36,
    p4: 40,
    p5: 44,
    p6: 52,
    p7: 64,
  },
};

const typeScaleSystem: TypeScaleSpecification<string> = {
  size: {
    base: `${typeScale.size.base / 16}rem`,
    n3: `${typeScale.size.n3 / 16}rem`,
    n2: `${typeScale.size.n2 / 16}rem`,
    n1: `${typeScale.size.n1 / 16}rem`,
    p1: `${typeScale.size.p1 / 16}rem`,
    p2: `${typeScale.size.p2 / 16}rem`,
    p3: `${typeScale.size.p3 / 16}rem`,
    p4: `${typeScale.size.p4 / 16}rem`,
    p5: `${typeScale.size.p5 / 16}rem`,
    p6: `${typeScale.size.p6 / 16}rem`,
    p7: `${typeScale.size.p7 / 16}rem`,
  },
  lineHeight: {
    base: `${typeScale.lineHeight.base / 16}rem`,
    n3: `${typeScale.lineHeight.n3 / 16}rem`,
    n2: `${typeScale.lineHeight.n2 / 16}rem`,
    n1: `${typeScale.lineHeight.n1 / 16}rem`,
    p1: `${typeScale.lineHeight.p1 / 16}rem`,
    p2: `${typeScale.lineHeight.p2 / 16}rem`,
    p3: `${typeScale.lineHeight.p3 / 16}rem`,
    p4: `${typeScale.lineHeight.p4 / 16}rem`,
    p5: `${typeScale.lineHeight.p5 / 16}rem`,
    p6: `${typeScale.lineHeight.p6 / 16}rem`,
    p7: `${typeScale.lineHeight.p7 / 16}rem`,
  },
};

const body: NominalScale = {
  large: {
    tracking: 0.1,
    size: typeScaleSystem.size.base,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.normal,
    lineHeight: typeScaleSystem.lineHeight.base,
    letterSpacing: math(`0.1 / ${typeScale.size.base} * 1rem`),
    font: `${typefaceReference.weights.normal} ${typeScaleSystem.size.base} ${typefaceReference.brand}`,
  },
  medium: {
    tracking: 0,
    size: typeScaleSystem.size.n1,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.normal,
    lineHeight: typeScaleSystem.lineHeight.n1,
    letterSpacing: math(`0 / ${typeScale.size.n1} * 1rem`),
    font: `${typefaceReference.weights.normal} ${typeScaleSystem.size.n1} ${typefaceReference.brand}`,
  },
  small: {
    tracking: 0,
    size: typeScaleSystem.size.n2,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.normal,
    lineHeight: typeScaleSystem.lineHeight.n2,
    letterSpacing: math(`0 / ${typeScale.size.n2} * 1rem`),
    font: `${typefaceReference.weights.normal} ${typeScaleSystem.size.n2} ${typefaceReference.brand}`,
  },
};

const display: NominalScale = {
  large: {
    tracking: 0,
    size: typeScaleSystem.size.p7,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.normal,
    lineHeight: typeScaleSystem.lineHeight.p7,
    letterSpacing: math(`0 / ${typeScale.size.p7} * 1rem`),
    font: `${typefaceReference.weights.normal} ${typeScaleSystem.size.p7} ${typefaceReference.brand}`,
  },
  medium: {
    tracking: 0,
    size: typeScaleSystem.size.p6,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.normal,
    lineHeight: typeScaleSystem.lineHeight.p6,
    letterSpacing: math(`0 / ${typeScale.size.p6} * 1rem`),
    font: `${typefaceReference.weights.normal} ${typeScaleSystem.size.p6} ${typefaceReference.brand}`,
  },
  small: {
    tracking: 0,
    size: typeScaleSystem.size.p5,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.normal,
    lineHeight: typeScaleSystem.lineHeight.p5,
    letterSpacing: math(`0 / ${typeScale.size.p5} * 1rem`),
    font: `${typefaceReference.weights.normal} ${typeScaleSystem.size.p5} ${typefaceReference.brand}`,
  },
};

const headline: NominalScale = {
  large: {
    tracking: 0,
    size: typeScaleSystem.size.p4,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.bold,
    lineHeight: typeScaleSystem.lineHeight.p4,
    letterSpacing: math(`0 / ${typeScale.size.p4} * 1rem`),
    font: `${typefaceReference.weights.bold} ${typeScaleSystem.size.p4} ${typefaceReference.brand}`,
  },
  medium: {
    tracking: 0,
    size: typeScaleSystem.size.p3,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.normal,
    lineHeight: typeScaleSystem.lineHeight.p3,
    letterSpacing: math(`0 / ${typeScale.size.p3} * 1rem`),
    font: `${typefaceReference.weights.normal} ${typeScaleSystem.size.p3} ${typefaceReference.brand}`,
  },
  small: {
    tracking: 0,
    size: typeScaleSystem.size.p2,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.normal,
    lineHeight: typeScaleSystem.lineHeight.p2,
    letterSpacing: math(`0 / ${typeScale.size.p2} * 1rem`),
    font: `${typefaceReference.weights.normal} ${typeScaleSystem.size.p2} ${typefaceReference.brand}`,
  },
};

const label: NominalScale = {
  large: {
    tracking: 0.1,
    size: typeScaleSystem.size.n1,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.medium,
    lineHeight: typeScaleSystem.lineHeight.n1,
    letterSpacing: math(`0.1 / ${typeScale.size.n1} * 1rem`),
    font: `${typefaceReference.weights.medium} ${typeScaleSystem.size.n1} ${typefaceReference.brand}`,
  },
  medium: {
    tracking: 0.5,
    size: typeScaleSystem.size.n2,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.medium,
    lineHeight: typeScaleSystem.lineHeight.n2,
    letterSpacing: math(`0.5 / ${typeScale.size.n2} * 1rem`),
    font: `${typefaceReference.weights.medium} ${typeScaleSystem.size.n2} ${typefaceReference.brand}`,
  },
  small: {
    tracking: 0.5,
    size: typeScaleSystem.size.n2,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.medium,
    lineHeight: typeScaleSystem.lineHeight.n3,
    letterSpacing: math(`0.5 / ${typeScale.size.n2} * 1rem`),
    font: `${typefaceReference.weights.medium} ${typeScaleSystem.size.n3} ${typefaceReference.brand}`,
  },
};

const title: NominalScale = {
  large: {
    tracking: 0,
    size: typeScaleSystem.size.p1,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.normal,
    lineHeight: typeScaleSystem.lineHeight.p1,
    letterSpacing: math(`0 / ${typeScale.size.p1} * 1rem`),
    font: `${typefaceReference.weights.normal} ${typeScaleSystem.size.p1} ${typefaceReference.brand}`,
  },
  medium: {
    tracking: 0.15,
    size: typeScaleSystem.size.base,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.medium,
    lineHeight: typeScaleSystem.lineHeight.base,
    letterSpacing: math(`0.15 / ${typeScale.size.base} * 1rem`),
    font: `${typefaceReference.weights.medium} ${typeScaleSystem.size.base} ${typefaceReference.brand}`,
  },
  small: {
    tracking: 0.1,
    size: typeScaleSystem.size.n1,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.medium,
    lineHeight: typeScaleSystem.lineHeight.n1,
    letterSpacing: math(`0.1 / ${typeScale.size.n1} * 1rem`),
    font: `${typefaceReference.weights.medium} ${typeScaleSystem.size.n1} ${typefaceReference.brand}`,
  },
};

const roles: TypographyRoles = {
  display: display,
  headline: headline,
  title: title,
  body: body,
  label: label,
};

export const typography: TypographySystem = {
  typeface: typefaceReference,
  typeScale: typeScale,
  typeScaleSystem: typeScaleSystem,
  roles: roles,
};
