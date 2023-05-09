import { Flexbox } from "../../../complib/layouts";
import { Text } from "../../../complib/text";
import { FormUnitHelper } from "./types/FormUnitHelper";
import styled from "styled-components/macro";
import Badge from "../../ui/badges/Badge";

interface UnitContainerProps {
  isDefault?: boolean;
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
  max-width: 40rem;
  max-height: 20rem;
`;

const NameAndUnitContainer = styled.span`
  display: flex;
  flex-direction: row;
  gap: 1rem;
  align-items: center;
`;

interface UnitPreviewProps {
  unit: Partial<FormUnitHelper>;
  noBadge?: boolean;
}

export default function UnitPreview({ unit, noBadge }: UnitPreviewProps) {
  return (
    <StyledUnit key={unit.unitId} isDefault={unit.isDefault}>
      <Flexbox justifyContent={"space-between"}>
        <NameAndUnitContainer>
          <Text fontSize={"24px"}>{unit.name}</Text>
          <Text color={"gray"}>{unit.symbol}</Text>
        </NameAndUnitContainer>
        {unit.isDefault && !noBadge && <Badge variant={"success"}>default</Badge>}
      </Flexbox>
      <p>{unit.description}</p>
    </StyledUnit>
  );
}
