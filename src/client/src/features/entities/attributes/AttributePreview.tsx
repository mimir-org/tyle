import styled from "styled-components/macro";
import { Control, useWatch } from "react-hook-form";
import { FormAttributeLib } from "./types/formAttributeLib";
import { Text } from "../../../complib/text";
import { Flexbox } from "../../../complib/layouts";
import { useTheme } from "styled-components";

const StyledDiv = styled.div`
  display: flex;
  flex-direction: column;
  gap: 1rem;
  padding: 1rem;
  border: 1px solid #ccc;
  height: fit-content;
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  background-color: ${(props) => props.theme.tyle.color.sys.surface.tint.base};
  box-shadow: ${(props) => props.theme.tyle.shadow.small};
  width: 70%;
  max-width: 40rem;
`;

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
`;

interface attributePreviewProps {
  control: Control<FormAttributeLib>;
}

export default function AttributePreview({ control }: attributePreviewProps) {
  const name = useWatch({ control, name: "name" });
  const description = useWatch({ control, name: "description" });
  const attributeUnits = useWatch({ control, name: "attributeUnits" });
  const theme = useTheme();

  return (
    <StyledDiv>
      <Text color={theme.tyle.color.sys.pure.base} variant={"headline-large"}>
        {name}
      </Text>
      <Text color={theme.tyle.color.sys.pure.base}>{description}</Text>
      {attributeUnits &&
        attributeUnits.map((unit) => (
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
        ))}
    </StyledDiv>
  );
}
