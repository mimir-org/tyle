import { typeScaleReference } from "../reference/typeScaleReference";
import { TypeScaleSpecification } from "../types";

/**
 * A collection of rem units for size and line-height
 * Each containing a range from n(x) - p(x) where n = negative, p = positive, x = number
 */
export const typeScaleSystem: TypeScaleSpecification<string> = {
  size: {
    base: `${typeScaleReference.size.base / 16}rem`,
    n3: `${typeScaleReference.size.n3 / 16 }rem`,
    n2: `${typeScaleReference.size.n2 / 16 }rem`,
    n1: `${typeScaleReference.size.n1 / 16 }rem`,
    p1: `${typeScaleReference.size.p1 / 16 }rem`,
    p2: `${typeScaleReference.size.p2 / 16 }rem`,
    p3: `${typeScaleReference.size.p3 / 16 }rem`,
    p4: `${typeScaleReference.size.p4 / 16 }rem`,
    p5: `${typeScaleReference.size.p5 / 16 }rem`,
    p6: `${typeScaleReference.size.p6 / 16 }rem`,
    p7: `${typeScaleReference.size.p7 / 16 }rem`,
  },
  lineHeight: {
    base: `${typeScaleReference.lineHeight.base / 16}rem`,
    n3: `${typeScaleReference.lineHeight.n3 / 16}rem`,
    n2: `${typeScaleReference.lineHeight.n2 / 16}rem`,
    n1: `${typeScaleReference.lineHeight.n1 / 16}rem`,
    p1: `${typeScaleReference.lineHeight.p1 / 16}rem`,
    p2: `${typeScaleReference.lineHeight.p2 / 16}rem`,
    p3: `${typeScaleReference.lineHeight.p3 / 16}rem`,
    p4: `${typeScaleReference.lineHeight.p4 / 16}rem`,
    p5: `${typeScaleReference.lineHeight.p5 / 16}rem`,
    p6: `${typeScaleReference.lineHeight.p6 / 16}rem`,
    p7: `${typeScaleReference.lineHeight.p7 / 16}rem`,
  }
}