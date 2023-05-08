import styled from "styled-components/macro";
import { Text } from "../../../complib/text";
import { useTheme } from "styled-components";
import { FormUnitHelper } from "../units/types/FormUnitHelper";
import UnitPreview from "../units/UnitPreview";

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
  overflow-y: auto;
  scrollbar-width: thin;
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
    <StyledDiv>
      <Text color={theme.tyle.color.sys.pure.base} variant={"headline-large"}>
        {name}
      </Text>
      <Text color={theme.tyle.color.sys.pure.base}>{description}</Text>
      {attributeUnits &&
        (small
          ? attributeUnits.filter((unit) => unit.isDefault).map((unit) => <UnitPreview unit={unit} key={unit.unitId} />)
          : attributeUnits.map((unit) => <UnitPreview unit={unit} key={unit.unitId} />))}
    </StyledDiv>
  );
}
