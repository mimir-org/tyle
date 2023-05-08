import { Flexbox } from "../../../complib/layouts";
import { Text } from "../../../complib/text";
import { FormUnitHelper } from "./types/FormUnitHelper";
import styled from "styled-components/macro";

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
  background-color: ${(props) =>
    props.isDefault ? props.theme.tyle.color.sys.surface.variant.base : props.theme.tyle.color.sys.surface.base};
  max-width: 40rem;
`;

const NameAndUnitContainer = styled.span`
  display: flex;
  flex-direction: row;
  gap: 1rem;
  align-items: center;
`;

const StyledBadge = styled.span`
  align-items: center;
  border-radius: 99999px;
  color: ${(props) => props.theme.tyle.color.sys.badge.success.on};
  padding: 0 8px 0 8px;
  background-color: ${(props) => props.theme.tyle.color.sys.badge.success.base};
  border: 1px solid ${(props) => props.theme.tyle.color.sys.badge.success.on};
  max-height: 1.5rem;
`;

interface UnitPreviewProps {
  unit: FormUnitHelper;
}

export default function UnitPreview({ unit }: UnitPreviewProps) {
  return (
    <StyledUnit key={unit.unitId} isDefault={unit.isDefault}>
      <Flexbox justifyContent={"space-between"}>
        <NameAndUnitContainer>
          <Text fontSize={"24px"}>{unit.name}</Text>
          <Text color={"gray"}>{unit.symbol}</Text>
        </NameAndUnitContainer>
        {unit.isDefault && <StyledBadge>default</StyledBadge>}
      </Flexbox>
      <p>{unit.description}</p>
    </StyledUnit>
  );
}
