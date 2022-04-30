import { TypeScaleDimension } from "./typeScaleDimension";

export interface TypeScaleSpecification<T> {
  size: TypeScaleDimension<T>,
  lineHeight: TypeScaleDimension<T>
}