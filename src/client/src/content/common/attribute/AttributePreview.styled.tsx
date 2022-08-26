import styled from "styled-components/macro";
import { Divider } from "../../../complib/data-display";
import { Palette } from "../../../complib/props";

export const AttributePreviewContainer = styled.div`
  display: flex;
  flex-direction: column;
  width: 250px;
  min-height: 150px;
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  border: 1px solid ${(props) => props.theme.tyle.color.sys.outline.base};
  background-color: ${(props) => props.theme.tyle.color.sys.surface.base};
`;

export const AttributePreviewHeader = styled.div<Pick<Palette, "bgColor">>`
  width: 100%;
  border-top-left-radius: inherit;
  border-top-right-radius: inherit;
  padding: ${(props) => props.theme.tyle.spacing.base};
  padding-left: ${(props) => props.theme.tyle.spacing.xl};
  padding-right: ${(props) => props.theme.tyle.spacing.xl};
  background-color: ${(props) => props.bgColor};
`;

export const AttributePreviewDivider = styled(Divider)`
  background-color: ${(props) => props.theme.tyle.color.sys.background.on};
  grid-column: 1 / 4;
`;
