import { ElementType } from "react";

export interface Polymorphic<T extends ElementType> {
  as?: T;
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
