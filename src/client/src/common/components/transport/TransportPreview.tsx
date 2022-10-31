import {
  Transport,
  TransportPreviewContainer,
  TransportPreviewHeader,
  TransportPreviewVariant,
} from "common/components/transport/TransportPreview.styled";
import { TextTypes } from "complib/props";
import { Text } from "complib/text";
import { useTheme } from "styled-components";

interface TransportPreviewProps {
  name: string;
  aspectColor?: string;
  transportColor?: string;
  variant?: TransportPreviewVariant;
}

/**
 * Components which presents a visual representation of a transport
 *
 * @param name
 * @param aspectColor
 * @param transportColor
 * @param variant
 * @constructor
 */
export const TransportPreview = ({ name, aspectColor, transportColor, variant = "small" }: TransportPreviewProps) => {
  const theme = useTheme();
  const headerTextVariant: TextTypes = variant == "small" ? "title-small" : "title-medium";
  const transportColorToDisplay = transportColor?.length ? transportColor : "rgba(0,0,0,0)";

  return (
    <TransportPreviewContainer variant={variant}>
      <TransportPreviewHeader bgColor={aspectColor}>
        <Text color={theme.tyle.color.ref.neutral["0"]} variant={headerTextVariant} width={"100%"} useEllipsis>
          {name}
        </Text>
      </TransportPreviewHeader>
      <Transport fill={transportColorToDisplay} />
    </TransportPreviewContainer>
  );
};
