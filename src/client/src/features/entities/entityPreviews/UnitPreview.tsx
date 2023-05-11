import { Flexbox } from "../../../complib/layouts";
import { Text } from "../../../complib/text";
import styled from "styled-components/macro";
import Badge from "../../ui/badges/Badge";

interface UnitContainerProps {
  isDefault?: boolean;
  small?: boolean;
}

const StyledUnit = styled.div<UnitContainerProps>`
  display: flex;
  flex-direction: column;
  border: 1px solid #ccc;
  gap: ${(props) => props.theme.tyle.spacing.l};
  padding: ${(props) => props.theme.tyle.spacing.l};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  height: fit-content;
  background-color: ${(props) =>
    props.isDefault ? props.theme.tyle.color.sys.surface.variant.base : props.theme.tyle.color.sys.surface.base};
  max-width: ${(props) => (props.small ? "200px" : "100%")};
`;

interface UnitPreviewProps {
  name: string;
  description: string;
  isDefault?: boolean;
  unitId?: string;
  symbol: string;
  noBadge?: boolean;
  small?: boolean;
}

export default function UnitPreview({
  name,
  description,
  unitId,
  isDefault,
  symbol,
  noBadge,
  small,
}: UnitPreviewProps) {
  return (
    <StyledUnit key={unitId} small={small} isDefault={isDefault}>
      <Flexbox justifyContent={"space-between"}>
        <Flexbox gap={"1rem"} alignItems={"center"}>
          <Text variant={small ? "title-small" : "display-small"}>{name}</Text>
          <Text variant={small ? "title-small" : "title-large"} color={"gray"}>
            {symbol}
          </Text>
        </Flexbox>
        {isDefault && !noBadge && <Badge variant={"success"}>default</Badge>}
      </Flexbox>
      <Text variant={small ? "body-small" : "body-large"} useEllipsis={small}>
        {description}
      </Text>
    </StyledUnit>
  );
}
