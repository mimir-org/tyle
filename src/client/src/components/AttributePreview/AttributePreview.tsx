import AttributeIcon from "components/AttributeIcon";
import Flexbox from "components/Flexbox";
import Text from "components/Text";
import { useTheme } from "styled-components";
import styled from "styled-components/macro";
import { State } from "types/common/state";

interface StyledDivProps {
  small?: boolean;
}

const StyledDiv = styled.div<StyledDivProps>`
  display: flex;
  flex-direction: column;
  gap: ${(props) => (props.small ? props.theme.tyle.spacing.xs : props.theme.tyle.spacing.xl)};
  padding: ${(props) => props.theme.tyle.spacing.xl};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  background-color: ${(props) => (props.small ? props.theme.tyle.color.pure.base : props.theme.tyle.color.tertiary.on)};
  border: 1px solid ${(props) => props.theme.tyle.color.outline.base};
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
  small?: boolean;
  state?: State;
}

const AttributePreview = ({ name, description, small }: AttributePreviewProps) => {
  const theme = useTheme();

  return (
    <StyledDiv small={small}>
      {small ? (
        AttributeSmallPreview("Attribute")
      ) : (
        <>
          <Flexbox justifyContent={"space-between"}>
            <Text
              color={theme.tyle.color.pure.base}
              variant={small ? "body-medium" : "headline-small"}
              useEllipsis={small}
            >
              {name}
            </Text>
          </Flexbox>
          {!small && <Text color={theme.tyle.color.pure.base}>{description}</Text>}
        </>
      )}
    </StyledDiv>
  );
};

export default AttributePreview;

const AttributeSmallPreview = (defaultAttributeSymbol: string) => {
  const theme = useTheme();
  return (
    <Flexbox justifyContent={"center"} alignItems={"center"} flexDirection={"column"} gap={theme.tyle.spacing.base}>
      <AttributeIcon color={theme.tyle.color.pure.on} />
      <Text variant={"title-medium"} textAlign={"center"}>
        {defaultAttributeSymbol}
      </Text>
    </Flexbox>
  );
};
