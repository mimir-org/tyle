import styled, { useTheme } from "styled-components/macro";
import { Text } from "../../../../complib/text";
import Badge from "../../../ui/badges/Badge";
import { Flexbox } from "../../../../complib/layouts";
import QuantityDatumIcon from "../../../icons/QuantityDatumIcon";

interface StyledDivProps {
  small?: boolean;
}

const StyledDiv = styled.div<StyledDivProps>`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.xl};
  padding: ${(props) => props.theme.tyle.spacing.xl};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  background-color: ${(props) => props.theme.tyle.color.sys.pure.base};
  max-width: ${(props) => (props.small ? "200px" : "100%")};
  width: ${(props) => (props.small ? "200px" : "100%")};
  border: ${(props) => props.theme.tyle.color.sys.outline.base} solid 1px};
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
      {small ? (
        QuantityDatumSmallPreview(quantityDatumTypeString[quantityDatumType ?? 0])
      ) : (
        <>
          <Text variant={"display-small"} useEllipsis={small}>
            {name}
          </Text>
          {quantityDatumType !== undefined ? (
            <Badge variant={"info"}>
              <Text variant={"body-medium"}>{quantityDatumTypeString[quantityDatumType]}</Text>
            </Badge>
          ) : null}
          <Text useEllipsis={small} variant={"body-large"}>
            {description}
          </Text>
        </>
      )}
    </StyledDiv>
  );
}

export const QuantityDatumSmallPreview = (quantityDatumType: string): JSX.Element => {
  const theme = useTheme();
  return (
    <Flexbox justifyContent={"center"} alignItems={"center"} flexDirection={"column"} gap={theme.tyle.spacing.base}>
      <QuantityDatumIcon color={theme.tyle.color.sys.pure.on} />
      <Text variant={"title-medium"} textAlign={"center"}>
        {quantityDatumType}
      </Text>
    </Flexbox>
  );
};
