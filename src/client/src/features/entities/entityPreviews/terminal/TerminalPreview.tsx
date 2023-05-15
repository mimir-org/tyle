import { TextTypes } from "complib/props";
import { Text } from "complib/text";
import { TerminalButton } from "features/common/terminal/TerminalButton";
import {
  TerminalPreviewContainer,
  TerminalPreviewVariant,
} from "features/entities/entityPreviews/terminal/TerminalPreview.styled";

interface AttributePreviewProps {
  name: string;
  color: string;
  variant?: TerminalPreviewVariant;
}

/**
 * Components which presents a visual representation of a terminal
 *
 * @param name
 * @param color
 * @param variant
 * @constructor
 */
export const TerminalPreview = ({ name, color, variant = "small" }: AttributePreviewProps) => {
  const headerTextVariant: TextTypes = variant == "small" ? "label-small" : "label-large";
  const colorToShow = color.length ? color : "rgba(0,0,0,0)";

  return (
    <TerminalPreviewContainer variant={variant}>
      <Text variant={headerTextVariant} width={"100%"} textAlign={"center"} useEllipsis>
        {name}
      </Text>
      <TerminalButton as={"span"} variant={"large"} color={colorToShow} />
    </TerminalPreviewContainer>
  );
};
