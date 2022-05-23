import styled, { css } from "styled-components/macro";
import { hideScrollbar } from "../../../../../../complib/mixins";

export const NodePanelPropertiesContainer = styled.div`
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.large};

  // Hidden scrollbar
  overflow: auto;
  ${hideScrollbar};

  // Shadow on scroll
  background: ${(props) => css`
    linear-gradient(${props.theme.tyle.color.surface.variant.base} 30%, rgba(255, 255, 255, 0)),
    linear-gradient(rgba(255, 255, 255, 0), ${props.theme.tyle.color.surface.variant.base} 70%) 0 100%,
    radial-gradient(farthest-side at 50% 0, rgba(0, 0, 0, .2), rgba(0, 0, 0, 0)),
    radial-gradient(farthest-side at 50% 100%, rgba(0, 0, 0, .2), rgba(0, 0, 0, 0)) 0 100%
  `};
  background-repeat: no-repeat;
  background-attachment: local, local, scroll, scroll;
  background-size: 100% 40px, 100% 40px, 100% 14px, 100% 14px;
`;
