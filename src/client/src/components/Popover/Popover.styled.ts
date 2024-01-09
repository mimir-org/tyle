import { translucify } from "helpers/theme.helpers";
import { focus, paletteMixin, sizingMixin } from "styleConstants";
import styled from "styled-components";
import { Palette, Sizing } from "types/styleProps";

export type PopoverContentProps = Sizing & Palette;

export const PopoverContent = styled.div<PopoverContentProps>`
  padding: ${(props) => props.theme.tyle.spacing.xl};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  background-color: ${(props) => props.theme.tyle.color.primary.base};
  color: ${(props) => props.theme.tyle.color.surface.base};
  box-shadow: ${(props) => props.theme.tyle.shadow.small};
  ${paletteMixin};
  ${sizingMixin};
  ${focus};
`;
