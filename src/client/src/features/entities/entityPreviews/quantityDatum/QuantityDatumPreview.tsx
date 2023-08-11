import styled, { useTheme } from "styled-components/macro";
import Badge from "../../../ui/badges/Badge";
import { Flexbox, Text } from "@mimirorg/component-library";
import QuantityDatumIcon from "../../../icons/QuantityDatumIcon";

interface StyledDivProps {
  small?: boolean;
}

const StyledDiv = styled.div<StyledDivProps>`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.mimirorg.spacing.xl};
  padding: ${(props) => props.theme.mimirorg.spacing.xl};
  border-radius: ${(props) => props.theme.mimirorg.border.radius.large};
  background-color: ${(props) => props.theme.mimirorg.color.pure.base};
  max-width: ${(props) => (props.small ? "200px" : "100%")};
  width: ${(props) => (props.small ? "200px" : "100%")};
  border: ${(props) => props.theme.mimirorg.color.outline.base} solid 1px};
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
          <Flexbox justifyContent={"space-between"} alignItems={"center"}>
            <Text variant={"display-small"} useEllipsis={small}>
              {name}
            </Text>
          </Flexbox>
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
    <Flexbox justifyContent={"center"} alignItems={"center"} flexDirection={"column"} gap={theme.mimirorg.spacing.base}>
      <QuantityDatumIcon color={theme.mimirorg.color.pure.on} />
      <Text variant={"title-medium"} textAlign={"center"}>
        {quantityDatumType}
      </Text>
    </Flexbox>
  );
};
