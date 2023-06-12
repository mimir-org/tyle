import styled from "styled-components/macro";
import { Text } from "../../../../complib/text";
import { useTheme } from "styled-components";
import { FormUnitHelper } from "../../units/types/FormUnitHelper";
import UnitPreview from "../unit/UnitPreview";
import { Flexbox } from "../../../../complib/layouts";
import AttributeIcon from "../../../icons/AttributeIcon";
import { State } from "@mimirorg/typelibrary-types";

interface StyledDivProps {
  small?: boolean;
}

const StyledDiv = styled.div<StyledDivProps>`
  display: flex;
  flex-direction: column;
  gap: ${(props) => (props.small ? props.theme.tyle.spacing.xs : props.theme.tyle.spacing.xl)};
  padding: ${(props) => props.theme.tyle.spacing.xl};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  background-color: ${(props) =>
    props.small ? props.theme.tyle.color.sys.pure.base : props.theme.tyle.color.sys.tertiary.on};
  border: 1px solid ${(props) => props.theme.tyle.color.sys.outline.base};
  max-width: 40rem;
  height: fit-content;
  overflow-y: auto;
  scrollbar-width: thin;
  width: ${(props) => (props.small ? "200px" : "40rem")};
  cursor: ${(props) => (props.small ? "pointer" : "auto")};
`;

interface AttributePreviewProps {
  name: string;
  description: string;
  units?: FormUnitHelper[];
  defaultUnit?: FormUnitHelper | null;
  small?: boolean;
  state?: State;
}

export default function AttributePreview({ name, description, units, defaultUnit, small }: AttributePreviewProps) {
  const theme = useTheme();
  units && units.sort((a) => (a.unitId === defaultUnit?.unitId ? -1 : 1));

  return (
    <StyledDiv small={small}>
      {small ? (
        AttributeSmallPreview(defaultUnit?.name ?? "Attribute")
      ) : (
        <>
          <Flexbox justifyContent={"space-between"}>
            <Text
              color={theme.tyle.color.sys.pure.base}
              variant={small ? "body-medium" : "headline-small"}
              useEllipsis={small}
            >
              {name}
            </Text>
          </Flexbox>
          {!small && <Text color={theme.tyle.color.sys.pure.base}>{description}</Text>}
          {units &&
            (small
              ? units
                  .filter((unit) => unit.unitId === defaultUnit?.unitId)
                  .map((unit) => <UnitPreview {...unit} key={unit.unitId} small={small} />)
              : units.map((unit) => (
                  <UnitPreview
                    {...unit}
                    key={unit.unitId}
                    isDefault={unit.unitId === defaultUnit?.unitId}
                    state={unit.state}
                    stateBadge
                  />
                )))}
        </>
      )}
    </StyledDiv>
  );
}

const AttributeSmallPreview = (defaultAttributeSymbol: string) => {
  const theme = useTheme();
  return (
    <Flexbox justifyContent={"center"} alignItems={"center"} flexDirection={"column"} gap={theme.tyle.spacing.base}>
      <AttributeIcon color={theme.tyle.color.sys.pure.on} />
      <Text variant={"title-medium"} textAlign={"center"}>
        {defaultAttributeSymbol}
      </Text>
    </Flexbox>
  );
};
