import styled from "styled-components/macro";
import { Text } from "../../../complib/text";
import { useTheme } from "styled-components";
import { FormUnitHelper } from "../units/types/FormUnitHelper";
import UnitPreview from "./UnitPreview";

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
  attributeUnits?: FormUnitHelper[];
  small?: boolean;
}

export default function AttributePreview({ name, description, attributeUnits, small }: attributePreviewProps) {
  const theme = useTheme();
  attributeUnits && attributeUnits.sort((a) => (a.isDefault ? -1 : 1));

  return (
    <StyledDiv small={small}>
      <Text color={theme.tyle.color.sys.pure.base} variant={"headline-large"} useEllipsis={small}>
        {name}
      </Text>
      {!small && <Text color={theme.tyle.color.sys.pure.base}>{description}</Text>}
      {attributeUnits &&
        (small
          ? attributeUnits
              .filter((unit) => unit.isDefault)
              .map((unit) => <UnitPreview {...unit} key={unit.unitId} small={small} noBadge />)
          : attributeUnits.map((unit) => <UnitPreview {...unit} key={unit.unitId} />))}
    </StyledDiv>
  );
}
