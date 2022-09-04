import styled, { css } from "styled-components/macro";
import { largeNode } from "./variants/largeNode";
import { smallNode } from "./variants/smallNode";

export type NodeVariant = "small" | "large";

export interface NodeContainerProps {
  color?: string;
  variant?: NodeVariant;
}

export const NodeContainer = styled.div<NodeContainerProps>`
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
        return smallNode;
      }
      case "large": {
        return largeNode;
      }
    }
  }};
`;
