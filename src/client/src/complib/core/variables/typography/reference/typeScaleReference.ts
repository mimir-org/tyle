import { TypeScaleSpecification } from "complib/core/variables/typography/types";

/**
 * A collection of px units for size and line-height
 * Each containing a range from n(x) - p(x) where n = negative, p = positive, x = number
 */
export const typeScaleReference: TypeScaleSpecification<number> = {
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
    p7: 57
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
    p7: 64
  }
};