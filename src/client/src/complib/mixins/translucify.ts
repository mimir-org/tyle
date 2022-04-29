import { transparentize } from "polished";

/**
 * Mixin for setting the opacity of a color
 * @param color
 * @param opacity
 */
export const translucify = (color: string, opacity: number) => {
  return transparentize(1 - opacity, color);
};
