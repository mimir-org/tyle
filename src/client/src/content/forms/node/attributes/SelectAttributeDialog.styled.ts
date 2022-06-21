import styled from "styled-components/macro";
import { hideScrollbar } from "../../../../complib/mixins";

export const SelectContainer = styled.div`
  display: flex;
  gap: ${(props) => props.theme.tyle.spacing.xl};
  padding: ${(props) => props.theme.tyle.spacing.xl};
  height: 400px;
  max-width: 450px;

  background-color: ${(props) => props.theme.tyle.color.sys.surface.base};
  border: 1px solid ${(props) => props.theme.tyle.color.sys.outline.base};
  border-radius: ${(props) => props.theme.tyle.border.radius.medium};

  // Hidden scrollbar
  overflow: auto;
  ${hideScrollbar};
`;
