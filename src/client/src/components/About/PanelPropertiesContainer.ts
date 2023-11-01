import { hideScrollbar } from "@mimirorg/component-library";
import styled from "styled-components/macro";

const PanelPropertiesContainer = styled.div`
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.mimirorg.spacing.xxxl};

  overflow: auto;
  ${hideScrollbar};

  // Fade bottom of container
  mask-image: linear-gradient(to bottom, black 95%, transparent 100%);
`;

export default PanelPropertiesContainer;