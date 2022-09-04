import styled from "styled-components/macro";
import { hideScrollbar } from "../../../../../../complib/mixins";

export const NodePanelPropertiesContainer = styled.div`
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.xxxl};

  // Hidden scrollbar
  overflow: auto;
  ${hideScrollbar};

  // Fade bottom of container
  mask-image: linear-gradient(to bottom, black 95%, transparent 100%);
`;
