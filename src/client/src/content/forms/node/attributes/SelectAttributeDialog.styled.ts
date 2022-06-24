import styled from "styled-components/macro";
import { hideScrollbar } from "../../../../complib/mixins";

export const SelectContainer = styled.div`
  display: flex;

  height: 520px;
  max-width: 650px;

  padding-bottom: ${(props) => props.theme.tyle.spacing.xl};

  // Fade bottom of container
  mask-image: linear-gradient(to bottom, black 93%, transparent 100%);

  // Hidden scrollbar
  overflow: auto;
  ${hideScrollbar};
`;
