import { css } from "styled-components/macro";

/**
 * Mixin for layering one color over another using linear-gradients
 *
 * @example
 * // A green color being layered above the blue one
 * const layeredGradientColor = layer("green", "blue");
 *
 * @example
 * // Color layering can be helpful when representing different states in css.
 * // Below we see a color being layered above another to represent the hover state.
 * :hover {
 *   background: ${layer(
 *     translucify(color.surface.on, state.hover.opacity),
 *     translucify(color.surface.base, state.enabled.opacity)
 *   )};
 * }
 *
 * @param colors each color layer is laid above the following layer
 */
export const layer = (...colors: string[]) => {
  const gradientLayers = colors
    .map((color) => {
      return `linear-gradient(${color}, ${color})`;
    })
    .join(",");

  return css`
    ${gradientLayers}
  `;
};
