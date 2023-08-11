import { hideScrollbar } from "complib/mixins";
import styled from "styled-components/macro";

export const SelectContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: ${(props) => props.theme.mimirorg.spacing.xxxl};
  width: 100%;

  // Ghost spacing to avoid focus style clipping
  padding-top: ${(props) => props.theme.mimirorg.spacing.xs};
  margin-top: -${(props) => props.theme.mimirorg.spacing.xs};

  overflow: auto;
`;

export const SelectItemsContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  align-content: start;
  justify-content: center;
  gap: ${(props) => props.theme.mimirorg.spacing.xl};

  height: 520px;
  max-width: 650px;

  padding: ${(props) => props.theme.mimirorg.spacing.xs};
  padding-bottom: ${(props) => props.theme.mimirorg.spacing.xl};

  // Fade bottom of container
  mask-image: linear-gradient(to bottom, black 93%, transparent 100%);

  // Hidden scrollbar
  overflow: auto;
  ${hideScrollbar};
`;
