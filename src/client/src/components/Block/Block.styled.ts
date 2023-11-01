import { largeBlock } from "components/Block/largeBlock";
import { smallBlock } from "components/Block/smallBlock";
import styled, { css } from "styled-components/macro";

export type BlockVariant = "small" | "large";

export interface BlockContainerProps {
  color?: string;
  variant?: BlockVariant;
}

const BlockContainer = styled.div<BlockContainerProps>`
  display: flex;
  flex-direction: column;
  justify-content: start;
  align-items: center;
  padding: ${(props) => props.theme.mimirorg.spacing.base};
  border-radius: ${(props) => props.theme.mimirorg.border.radius.large};
  border: ${(props) => !props.color && css`1px solid ${props.theme.mimirorg.color.outline.base}`};
  background: ${(props) => (props.color ? props.color : props.theme.mimirorg.color.surface.base)};

  ${({ variant }) => {
    switch (variant) {
      case "small": {
        return smallBlock;
      }
      case "large": {
        return largeBlock;
      }
    }
  }};
`;

export default BlockContainer;
