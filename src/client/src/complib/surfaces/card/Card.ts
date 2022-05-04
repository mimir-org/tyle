import styled, { css } from "styled-components/macro";
import { motion } from "framer-motion";
import { ElementType } from "react";
import { layer, translucify } from "../../mixins";
import { Elevation, ElevationLevels, Polymorphic } from "../../props";
import { ColorSystem, ElevationSystem, ShadowSystem, StateSystem } from "../../core/";

type CardProps = Elevation &
  Polymorphic<ElementType> & {
    variant?: "elevated" | "filled" | "outlined";
    square?: boolean;
    interactive?: boolean;
  };

export const Card = styled.div<CardProps>`
  padding: ${(props) => props.theme.typeLibrary.spacing.small};
  border-radius: ${(props) => !props.square && props.theme.typeLibrary.border.radius.medium};

  ${({ variant, elevation, interactive, ...props }) => {
    const { color, state, elevation: elevationSystem, shadow } = props.theme.typeLibrary;

    switch (variant) {
      case "elevated": {
        return elevatedCard(color, state, shadow, elevationSystem, elevation, interactive);
      }
      case "filled": {
        return filledCard(color, state, elevationSystem, elevation, interactive);
      }
      case "outlined": {
        return outlinedCard(color, state, elevationSystem, elevation, interactive);
      }
    }
  }};
`;

Card.defaultProps = {
  variant: "filled",
};

const elevatedCard = (
  color: ColorSystem,
  state: StateSystem,
  shadow: ShadowSystem,
  elevationSystem: ElevationSystem,
  elevationLevel?: ElevationLevels,
  interactive?: boolean
) => css`
  box-shadow: ${shadow.small};
  background: ${layer(
    translucify(color.surface.tint.base, elevationSystem.levels[elevationLevel ?? 1].opacity),
    translucify(color.surface.base, state.enabled.opacity)
  )};

  ${interactive &&
  css`
    :hover {
      box-shadow: ${shadow.medium};
      background: ${layer(
        translucify(color.surface.on, state.hover.opacity),
        translucify(color.surface.base, state.enabled.opacity)
      )};
    }

    :active {
      background: ${layer(
        translucify(color.surface.on, state.pressed.opacity),
        translucify(color.surface.base, state.enabled.opacity)
      )};
    }
  `}
`;

const filledCard = (
  color: ColorSystem,
  state: StateSystem,
  elevationSystem: ElevationSystem,
  elevationLevel?: ElevationLevels,
  interactive?: boolean
) => css`
  background: ${layer(
    translucify(color.surface.tint.base, elevationSystem.levels[elevationLevel ?? 0].opacity),
    translucify(color.surface.variant.base, state.enabled.opacity)
  )};

  ${interactive &&
  css`
    :hover {
      background: ${layer(
        translucify(color.surface.on, state.hover.opacity),
        translucify(color.surface.variant.base, state.enabled.opacity)
      )};
    }

    :active {
      background: ${layer(
        translucify(color.surface.on, state.pressed.opacity),
        translucify(color.surface.variant.base, state.enabled.opacity)
      )};
    }
  `}
`;

const outlinedCard = (
  color: ColorSystem,
  state: StateSystem,
  elevationSystem: ElevationSystem,
  elevationLevel?: ElevationLevels,
  interactive?: boolean
) => css`
  border: 1px solid ${color.outline.base};
  background: ${layer(
    translucify(color.surface.tint.base, elevationSystem.levels[elevationLevel ?? 0].opacity),
    translucify(color.surface.base, state.enabled.opacity)
  )};

  ${interactive &&
  css`
    :hover {
      background: ${layer(
        translucify(color.surface.on, state.hover.opacity),
        translucify(color.surface.base, state.enabled.opacity)
      )};
    }

    :active {
      background: ${layer(
        translucify(color.surface.on, state.pressed.opacity),
        translucify(color.surface.base, state.enabled.opacity)
      )};
    }
  `}
`;

/**
 * An animation wrapper for the Card component
 *
 * @see https://github.com/framer/motion
 */
export const MotionCard = motion(Card, { forwardMotionProps: true });
