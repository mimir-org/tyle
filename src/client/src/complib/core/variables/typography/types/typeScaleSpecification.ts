import { TypeScaleDimension } from "complib/core/variables/typography/types/typeScaleDimension";

export interface TypeScaleSpecification<T> {
  size: TypeScaleDimension<T>,
  lineHeight: TypeScaleDimension<T>
}