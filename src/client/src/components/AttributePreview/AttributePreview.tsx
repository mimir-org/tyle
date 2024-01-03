import { Flexbox, Text } from "@mimirorg/component-library";
import AttributeIcon from "components/AttributeIcon";
import { useTheme } from "styled-components";
import styled from "styled-components/macro";
import { State } from "types/common/state";

interface StyledDivProps {
  small?: boolean;
}

const StyledDiv = styled.div<StyledDivProps>`
  display: flex;
  flex-direction: column;
  gap: ${(props) => (props.small ? props.theme.mimirorg.spacing.xs : props.theme.mimirorg.spacing.xl)};
  padding: ${(props) => props.theme.mimirorg.spacing.xl};
  border-radius: ${(props) => props.theme.mimirorg.border.radius.large};
  background-color: ${(props) =>
    props.small ? props.theme.mimirorg.color.pure.base : props.theme.mimirorg.color.tertiary.on};
  border: 1px solid ${(props) => props.theme.mimirorg.color.outline.base};
  max-height: 75vh;
  max-width: 40rem;
  height: fit-content;
  overflow-y: auto;
  scrollbar-width: thin;
  width: ${(props) => (props.small ? "200px" : "40rem")};
  cursor: ${(props) => (props.small ? "pointer" : "auto")};
`;

//TODO This component is also used in the search list. Clean up and remove aboutSection only code.
interface AttributePreviewProps {
  name: string;
  description: string;
  //units?: FormUnitHelper[];
  //defaultUnit?: FormUnitHelper | null;
  small?: boolean;
  state?: State;
}

const AttributePreview = ({ name, description, small }: AttributePreviewProps) => {
  const theme = useTheme();
  //units && units.sort((a) => (a.unitId === defaultUnit?.unitId ? -1 : 1));

  return (
    <StyledDiv small={small}>
      {small ? (
        AttributeSmallPreview(/*defaultUnit?.name ?? */ "Attribute")
      ) : (
        <>
          <Flexbox justifyContent={"space-between"}>
            <Text
              color={theme.mimirorg.color.pure.base}
              variant={small ? "body-medium" : "headline-small"}
              useEllipsis={small}
            >
              {name}
            </Text>
          </Flexbox>
          {!small && <Text color={theme.mimirorg.color.pure.base}>{description}</Text>}
          {/*units &&
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
              )))*/}
        </>
      )}
    </StyledDiv>
  );
};

export default AttributePreview;

const AttributeSmallPreview = (defaultAttributeSymbol: string) => {
  const theme = useTheme();
  return (
    <Flexbox justifyContent={"center"} alignItems={"center"} flexDirection={"column"} gap={theme.mimirorg.spacing.base}>
      <AttributeIcon color={theme.mimirorg.color.pure.on} />
      <Text variant={"title-medium"} textAlign={"center"}>
        {defaultAttributeSymbol}
      </Text>
    </Flexbox>
  );
};
