import styled from "styled-components/macro";
import { Text } from "../../../../complib/text";
import Badge from "../../../ui/badges/Badge";

interface StyledDivProps {
  small?: boolean;
}

const StyledDiv = styled.div<StyledDivProps>`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.xl};
  padding: ${(props) => props.theme.tyle.spacing.xl};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  background-color: ${(props) => props.theme.tyle.color.sys.surface.base};
  max-width: ${(props) => (props.small ? "200px" : "100%")};
  max-height: ${(props) => (props.small ? "200px" : "100%")};
  width: ${(props) => (props.small ? "200px" : "100%")};
`;

interface QuantityDatumPreviewProps {
  name: string;
  quantityDatumType?: number;
  description: string;
  small?: boolean;
}

const quantityDatumTypeString = ["Specified scope", "Specified provenance", "Specified range", "Specified regularity"];

export default function QuantityDatumPreview({
  name,
  quantityDatumType,
  description,
  small,
}: QuantityDatumPreviewProps) {
  return (
    <StyledDiv small={small}>
      <Text variant={small ? "title-large" : "display-small"} useEllipsis={small}>
        {name}
      </Text>
      <Text useEllipsis={small}>{description}</Text>
      {quantityDatumType !== undefined ? (
        <Badge variant={"info"}>
          <Text variant={small ? "body-small" : "body-medium"}>{quantityDatumTypeString[quantityDatumType]}</Text>
        </Badge>
      ) : null}
    </StyledDiv>
  );
}
