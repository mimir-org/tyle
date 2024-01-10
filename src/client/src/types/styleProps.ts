import { ElementType } from "react";

export interface Borders {
  border?: string | number;
  borderTop?: string | number;
  borderLeft?: string | number;
  borderRight?: string | number;
  borderBottom?: string | number;
  borderColor?: string;
  borderTopColor?: string;
  borderRightColor?: string;
  borderBottomColor?: string;
  borderLeftColor?: string;
  borderRadius?: string;
}

export interface Display {
  display?: string;
  overflow?: string;
  textOverflow?: string;
  visibility?: string;
  whiteSpace?: string;
}

export interface Ellipsis {
  useEllipsis?: boolean;
  ellipsisMaxLines?: number;
}

export interface Flex {
  flexDirection?: string;
  flexWrap?: string;
  justifyContent?: string;
  alignItems?: string;
  alignContent?: string;
  order?: string;
  flex?: string | number;
  flexGrow?: string;
  flexShrink?: string;
  flexFlow?: string;
  alignSelf?: string;
  gap?: string;
}

export interface Grid {
  gap?: string;
  columnGap?: string;
  rowGap?: string;
  gridColumn?: string;
  gridRow?: string;
  gridAutoFlow?: string;
  gridAutoColumns?: string;
  gridAutoRows?: string;
  gridTemplateColumns?: string;
  gridTemplateRows?: string;
  gridTemplateAreas?: string;
  gridArea?: string;
  justifyItems?: string;
  alignItems?: string;
  placeItems?: string;
  justifyContent?: string;
  alignContent?: string;
  placeContent?: string;
  justifySelf?: string;
  alignSelf?: string;
  placeSelf?: string;
}

export interface Palette {
  color?: string;
  bgColor?: string;
}

export interface Polymorphic<T extends ElementType> {
  as?: T;
}

export interface Positions {
  position?: string;
  zIndex?: string | number;
  top?: string | number;
  right?: string | number;
  bottom?: string | number;
  left?: string | number;
}

export interface Shadows {
  boxShadow?: string;
}

export interface Sizing {
  width?: string;
  maxWidth?: string;
  minWidth?: string;
  height?: string;
  maxHeight?: string;
  minHeight?: string;
  boxSizing?: string;
}

/**
 * m - margin
 * p - padding
 *
 * t - top
 * r - right
 * l - left
 * b - bottom
 *
 * x - left and right
 * y - top and bottom
 */
export interface Spacing {
  m?: string;
  mt?: string;
  mr?: string;
  mb?: string;
  ml?: string;
  mx?: string;
  my?: string;
  p?: string;
  pt?: string;
  pr?: string;
  pb?: string;
  pl?: string;
  px?: string;
  py?: string;
}

export interface Spacings {
  spacing?: Spacing;
}

type DisplayType = "display-large" | "display-medium" | "display-small";

type HeadlineType = "headline-large" | "headline-medium" | "headline-small";

type TitleType = "title-large" | "title-medium" | "title-small";

type BodyType = "body-large" | "body-medium" | "body-small";

type LabelType = "label-large" | "label-medium" | "label-small";

export type TextTypes = DisplayType | HeadlineType | TitleType | BodyType | LabelType;

export interface TextVariant {
  variant?: TextTypes;
}

export interface Typography {
  font?: string;
  fontFamily?: string;
  fontSize?: string;
  fontStyle?: string;
  fontWeight?: string | number;
  letterSpacing?: string;
  lineHeight?: string;
  textAlign?: string;
  textTransform?: string;
  wordBreak?: string;
}
