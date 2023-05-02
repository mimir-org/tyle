import { largeAspectObject } from "features/common/aspectobject/variants/largeAspectObject";
import { smallAspectObject } from "features/common/aspectobject/variants/smallAspectObject";
import styled, { css } from "styled-components/macro";

export type AspectObjectVariant = "small" | "large";

export interface AspectObjectContainerProps {
  color?: string;
  variant?: AspectObjectVariant;
}

export const AspectObjectContainer = styled.div<AspectObjectContainerProps>`
  display: flex;
  flex-direction: column;
  justify-content: start;
  align-items: center;
  padding: ${(props) => props.theme.tyle.spacing.xl};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  border: ${(props) => !props.color && css`1px solid ${props.theme.tyle.color.sys.outline.base}`};
  background: ${(props) => (props.color ? props.color : props.theme.tyle.color.sys.surface.base)};

  ${({ variant }) => {
    switch (variant) {
      case "small": {
        return smallAspectObject;
      }
      case "large": {
        return largeAspectObject;
      }
    }
  }};
`;
