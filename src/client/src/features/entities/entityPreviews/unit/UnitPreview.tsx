import { Flexbox, Text } from "@mimirorg/component-library";
import styled, { useTheme } from "styled-components/macro";
import UnitIcon from "../../../icons/UnitIcon";
import { StateBadge } from "../../../ui/badges/StateBadge";
import { State } from "@mimirorg/typelibrary-types";

interface UnitContainerProps {
  isDefault?: boolean;
  small?: boolean;
}

const StyledUnit = styled.div<UnitContainerProps>`
  display: flex;
  flex-direction: column;
  border: 1px solid #ccc;
  gap: ${(props) => props.theme.mimirorg.spacing.l};
  padding: ${(props) => props.theme.mimirorg.spacing.xl};
  border-radius: ${(props) => props.theme.mimirorg.border.radius.large};
  height: fit-content;
  background-color: ${(props) =>
    props.isDefault ? props.theme.mimirorg.color.surface.variant.base : props.theme.mimirorg.color.pure.base};
  max-width: ${(props) => (props.small ? "200px" : "auto")};
  width: 100%;
`;

interface UnitPreviewProps {
  name: string;
  description: string;
  isDefault?: boolean;
  unitId?: string;
  symbol: string;
  small?: boolean;
  state?: State;
  stateBadge?: boolean;
}

export default function UnitPreview({
  name,
  description,
  unitId,
  isDefault,
  symbol,
  small,
  state,
  stateBadge = false,
}: UnitPreviewProps) {
  return (
    <StyledUnit key={unitId} small={small} isDefault={isDefault}>
      {small ? (
        UnitSmallPreview(symbol)
      ) : (
        <>
          <Flexbox justifyContent={"space-between"}>
            <Text variant={"display-small"} useEllipsis={small}>
              {name}
              {isDefault ? " (default)" : null}
            </Text>
            <Flexbox flexDirection={"row"}>
              {state !== undefined && stateBadge ? <StateBadge state={state} /> : null}
            </Flexbox>
          </Flexbox>
          <Text variant={"title-medium"} color={"gray"}>
            {symbol}
          </Text>
          <Text variant={"body-large"} useEllipsis={small}>
            {description}
          </Text>
        </>
      )}
    </StyledUnit>
  );
}

const UnitSmallPreview = (symbol: string) => {
  const theme = useTheme();
  return (
    <Flexbox justifyContent={"center"} alignItems={"center"} flexDirection={"column"} gap={theme.mimirorg.spacing.base}>
      <UnitIcon color={theme.mimirorg.color.pure.on} />
      <Text variant={"title-medium"} textAlign={"center"}>
        {symbol}
      </Text>
    </Flexbox>
  );
};
