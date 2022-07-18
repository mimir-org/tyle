import styled from "styled-components/macro";
import { largeNode } from "./variants/largeNode";
import { smallNode } from "./variants/smallNode";

export type NodeVariant = "small" | "large";

export interface NodeContainerProps {
  color: string;
  variant?: NodeVariant;
}

export const NodeContainer = styled.div<NodeContainerProps>`
  display: flex;
  flex-direction: column;
  justify-content: start;
  align-items: center;
  padding: ${(props) => props.theme.tyle.spacing.xl};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  background: ${(props) => props.color};

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
