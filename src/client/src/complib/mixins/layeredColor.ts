import { css } from "styled-components/macro";
import { translucify } from "./translucify";

interface Layer {
  color: string;
  opacity: number;
}

/**
 * Mixin for layering one color over another using linear-gradients
 *
 * @example
 * // Here the color green will be layered above the blue color
 * const layeredGradientColor = layeredColor(
 *   {
 *     color: "green",
 *     opacity: 0.08,
 *   },
 *   { color: "blue", opacity: 1 }
 * );
 *
 * @example
 * // Color layering can be helpful when representing different states in css.
 * // Below we see a color being layered above the primary to represent the hover state.
 * :hover {
 *   background: ${layeredColor(
 *     {
 *       color: props.theme.typeLibrary.color.primary.on,
 *       opacity: 0.08,
 *     },
 *     { color: props.theme.typeLibrary.color.primary.base, opacity: 1 }
 *   )};
 * }
 *
 * @param layers each color layer is laid above the following layer
 */
export const layeredColor = (...layers: Layer[]) => {
  const gradientLayers = layers
    .map((layer) => {
      const transformed = translucify(layer.color, layer.opacity);
      return `linear-gradient(${transformed}, ${transformed})`;
    })
    .join(",");

  return css`
    ${gradientLayers}
  `;
};
