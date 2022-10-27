import { hideScrollbar } from "complib/mixins";
import styled from "styled-components/macro";

export const PanelPropertiesContainer = styled.div`
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.xxxl};

  overflow: auto;
  ${hideScrollbar};

  // Fade bottom of container
  mask-image: linear-gradient(to bottom, black 95%, transparent 100%);
`;
