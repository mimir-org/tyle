import TerminalButton from "components/TerminalButton";
import Text from "components/Text";
import { TextTypes } from "types/styleProps";
import TerminalPreviewContainer, { TerminalPreviewVariant } from "./TerminalPreview.styled";

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
const TerminalPreview = ({ name, color, variant = "small" }: AttributePreviewProps) => {
  const headerTextVariant: TextTypes = variant === "small" ? "title-medium" : "label-large";
  const colorToShow = color.length ? color : "rgba(0,0,0,0)";

  return (
    <TerminalPreviewContainer variant={variant}>
      <TerminalButton as={"span"} variant={"large"} color={colorToShow} />
      <Text variant={headerTextVariant} width={"100%"} textAlign={"center"} useEllipsis>
        {name}
      </Text>
    </TerminalPreviewContainer>
  );
};

export default TerminalPreview;
