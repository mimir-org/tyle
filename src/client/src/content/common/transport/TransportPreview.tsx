import { TextTypes } from "../../../complib/props";
import { Text } from "../../../complib/text";
import { Transport, TransportPreviewContainer, TransportPreviewVariant } from "./TransportPreview.styled";

interface TransportPreviewProps {
  name: string;
  color?: string;
  variant?: TransportPreviewVariant;
}

/**
 * Components which presents a visual representation of a transport
 *
 * @param name
 * @param color
 * @param variant
 * @constructor
 */
export const TransportPreview = ({ name, color, variant = "small" }: TransportPreviewProps) => {
  const headerTextVariant: TextTypes = variant == "small" ? "label-small" : "label-large";
  const colorToShow = color?.length ? color : "rgba(0,0,0,0)";

  return (
    <TransportPreviewContainer variant={variant}>
      <Text variant={headerTextVariant} width={"100%"} textAlign={"center"} useEllipsis>
        {name}
      </Text>
      <Transport fill={colorToShow} />
    </TransportPreviewContainer>
  );
};
