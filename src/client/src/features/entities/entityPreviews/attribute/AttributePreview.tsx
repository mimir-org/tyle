import styled from "styled-components/macro";
import { Text } from "../../../../complib/text";
import { useTheme } from "styled-components";
import { FormUnitHelper } from "../../units/types/FormUnitHelper";
import UnitPreview from "../unit/UnitPreview";

interface StyledDivProps {
  small?: boolean;
}

const StyledDiv = styled.div<StyledDivProps>`
  display: flex;
  flex-direction: column;
  gap: ${(props) => (props.small ? props.theme.tyle.spacing.xs : props.theme.tyle.spacing.xl)};
  padding: ${(props) => props.theme.tyle.spacing.xl};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  background-color: ${(props) => props.theme.tyle.color.sys.surface.tint.base};
  box-shadow: ${(props) => props.theme.tyle.shadow.small};
  max-width: 40rem;
  height: fit-content;
  overflow-y: auto;
  scrollbar-width: thin;
  width: ${(props) => (props.small ? "200px" : "40rem")};
  cursor: ${(props) => (props.small ? "pointer" : "auto")};
`;

interface attributePreviewProps {
  name: string;
  description: string;
  units?: FormUnitHelper[];
  defaultUnit?: FormUnitHelper;
  small?: boolean;
}

export default function AttributePreview({ name, description, units, defaultUnit, small }: attributePreviewProps) {
  const theme = useTheme();
  units && units.sort((a) => (a.unitId === defaultUnit?.unitId ? -1 : 1));

  return (
    <StyledDiv small={small}>
      <Text
        color={theme.tyle.color.sys.pure.base}
        variant={small ? "body-medium" : "headline-large"}
        useEllipsis={small}
      >
        {name}
      </Text>
      {!small && <Text color={theme.tyle.color.sys.pure.base}>{description}</Text>}
      {units &&
        (small
          ? units
              .filter((unit) => unit.unitId === defaultUnit?.unitId)
              .map((unit) => <UnitPreview {...unit} key={unit.unitId} small={small} noBadge />)
          : units.map((unit) => (
              <UnitPreview {...unit} key={unit.unitId} isDefault={unit.unitId === defaultUnit?.unitId} />
            )))}
    </StyledDiv>
  );
}
