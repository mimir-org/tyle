import { useTheme } from "styled-components";
import { TextTypes } from "../../../complib/props";
import { Text } from "../../../complib/text";
import {
  Interface,
  InterfacePreviewContainer,
  InterfacePreviewHeader,
  InterfacePreviewVariant,
} from "./InterfacePreview.styled";

interface InterfacePreviewProps {
  name: string;
  aspectColor?: string;
  interfaceColor?: string;
  variant?: InterfacePreviewVariant;
}

/**
 * Components which presents a visual representation of an interface
 *
 * @param name
 * @param aspectColor
 * @param interfaceColor
 * @param variant
 * @constructor
 */
export const InterfacePreview = ({ name, aspectColor, interfaceColor, variant = "small" }: InterfacePreviewProps) => {
  const theme = useTheme();
  const headerTextVariant: TextTypes = variant == "small" ? "title-small" : "title-medium";
  const interfaceColorToDisplay = interfaceColor?.length ? interfaceColor : "rgba(0,0,0,0)";

  return (
    <InterfacePreviewContainer variant={variant}>
      <InterfacePreviewHeader bgColor={aspectColor}>
        <Text color={theme.tyle.color.ref.neutral["0"]} variant={headerTextVariant} width={"100%"} useEllipsis>
          {name}
        </Text>
      </InterfacePreviewHeader>
      <Interface color={interfaceColorToDisplay} />
    </InterfacePreviewContainer>
  );
};
