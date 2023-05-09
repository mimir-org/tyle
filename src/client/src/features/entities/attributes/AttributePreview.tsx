import styled from "styled-components/macro";
import { Text } from "../../../complib/text";
import { useTheme } from "styled-components";
import { FormUnitHelper } from "../units/types/FormUnitHelper";
import UnitPreview from "../units/UnitPreview";

interface StyledDivProps {
  small?: boolean;
}

const StyledDiv = styled.div<StyledDivProps>`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.xl};
  padding: ${(props) => props.theme.tyle.spacing.xl};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  background-color: ${(props) => props.theme.tyle.color.sys.surface.tint.base};
  box-shadow: ${(props) => props.theme.tyle.shadow.small};
  max-width: 40rem;
  height: fit-content;
  overflow-y: auto;
  scrollbar-width: thin;
  transition: all 0.5s ease-in-out;
  transform: ${(props) => (props.small ? "scale(0.5)" : "scale(1)")};
  width: ${(props) => (props.small ? "300px" : "auto")};
  margin: ${(props) => (props.small ? "-5%" : "0")};
  cursor: ${(props) => (props.small ? "pointer" : "auto")};
`;

interface attributePreviewProps {
  name: string;
  description: string;
  attributeUnits?: FormUnitHelper[];
  small?: boolean;
}

export default function AttributePreview({ name, description, attributeUnits, small }: attributePreviewProps) {
  const theme = useTheme();
  attributeUnits && attributeUnits.sort((a) => (a.isDefault ? -1 : 1));

  return (
    <StyledDiv small={small}>
      <Text color={theme.tyle.color.sys.pure.base} variant={"headline-large"}>
        {name}
      </Text>
      <Text color={theme.tyle.color.sys.pure.base}>{description}</Text>
      {attributeUnits &&
        (small
          ? attributeUnits
              .filter((unit) => unit.isDefault)
              .map((unit) => <UnitPreview unit={unit} key={unit.unitId} noBadge />)
          : attributeUnits.map((unit) => <UnitPreview unit={unit} key={unit.unitId} />))}
    </StyledDiv>
  );
}
