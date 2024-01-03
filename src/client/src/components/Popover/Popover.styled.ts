import { translucify } from "helpers/theme.helpers";
import { focus, paletteMixin, sizingMixin } from "styleConstants";
import styled from "styled-components";
import { Palette, Sizing } from "types/styleProps";

export type PopoverContentProps = Sizing & Palette;

export const PopoverContent = styled.div<PopoverContentProps>`
  padding: ${(props) => props.theme.tyle.spacing.xl};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  background-color: ${(props) => translucify(props.theme.tyle.color.surface.base, 0.98)};
  color: ${(props) => props.theme.tyle.color.surface.on};
  box-shadow: ${(props) => props.theme.tyle.shadow.small};
  ${paletteMixin};
  ${sizingMixin};
  ${focus};
`;
