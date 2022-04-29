import styled, { css } from "styled-components/macro";
import { Elevation, ElevationLevels, Polymorphic } from "../../props";
import { ElementType } from "react";
import { layeredColor } from "../../mixins";
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
  box-shadow: ${shadow.boxSmall};
  background: ${layeredColor(
    {
      color: color.surface.tint.base,
      opacity: elevationSystem.levels[elevationLevel ?? 1].opacity,
    },
    { color: color.surface.base, opacity: 1 }
  )};

  ${interactive &&
  css`
    :hover {
      box-shadow: ${shadow.boxMedium};
      background: ${layeredColor(
        {
          color: color.surface.on,
          opacity: state.hover.opacity,
        },
        { color: color.surface.base, opacity: 1 }
      )};
    }

    :active {
      background: ${layeredColor(
        {
          color: color.surface.on,
          opacity: state.pressed.opacity,
        },
        { color: color.surface.base, opacity: 1 }
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
  background: ${layeredColor(
    {
      color: color.surface.tint.base,
      opacity: elevationSystem.levels[elevationLevel ?? 0].opacity,
    },
    { color: color.surface.variant.base, opacity: state.enabled.opacity }
  )};

  ${interactive &&
  css`
    :hover {
      background: ${layeredColor(
        {
          color: color.surface.on,
          opacity: state.hover.opacity,
        },
        { color: color.surface.variant.base, opacity: state.enabled.opacity }
      )};
    }

    :active {
      background: ${layeredColor(
        {
          color: color.surface.on,
          opacity: state.pressed.opacity,
        },
        { color: color.surface.variant.base, opacity: state.enabled.opacity }
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
  background: ${layeredColor(
    {
      color: color.surface.tint.base,
      opacity: elevationSystem.levels[elevationLevel ?? 0].opacity,
    },
    { color: color.surface.base, opacity: state.enabled.opacity }
  )};
  border: 1px solid ${color.outline.base};

  ${interactive &&
  css`
    :hover {
      background: ${layeredColor(
        {
          color: color.surface.on,
          opacity: state.hover.opacity,
        },
        { color: color.surface.base, opacity: state.enabled.opacity }
      )};
    }

    :active {
      background: ${layeredColor(
        {
          color: color.surface.on,
          opacity: state.pressed.opacity,
        },
        { color: color.surface.base, opacity: state.enabled.opacity }
      )};
    }
  `}
`;
