import styled, { useTheme } from "styled-components/macro";
import Badge from "../../../ui/badges/Badge";
import RdsIcon from "../../../icons/RdsIcon";
import { Flexbox, Text } from "@mimirorg/component-library";
import { State } from "@mimirorg/typelibrary-types";
import { StateBadge } from "../../../ui/badges/StateBadge";

interface RdsPreviewProps {
  name: string;
  description: string;
  rdsCode: string;
  small?: boolean;
  state?: State;
  stateBadge?: boolean;
}

interface StyledDivProps {
  small?: boolean;
}
const StyledDiv = styled.div<StyledDivProps>`
  display: flex;
  flex-direction: column;
  gap: ${(props) => props.theme.tyle.spacing.xl};
  padding: ${(props) => props.theme.tyle.spacing.xl};
  border-radius: ${(props) => props.theme.tyle.border.radius.large};
  background-color: ${(props) => props.theme.tyle.color.sys.pure.base};
  border: 1px solid ${(props) => props.theme.tyle.color.sys.outline.base};
  width: ${(props) => (props.small ? "200px" : "auto")};
  height: fit-content;
`;

export const RdsPreview = ({
  name,
  description,
  rdsCode,
  small,
  state,
  stateBadge = false,
}: RdsPreviewProps): JSX.Element => {
  return (
    <StyledDiv small={small}>
      {small ? (
        RdsSmallPreview(rdsCode)
      ) : (
        <>
          <Flexbox justifyContent={"space-between"}>
            <Text variant={"title-medium"} useEllipsis={small}>
              {name}
            </Text>
            {state !== undefined && stateBadge && <StateBadge state={state} />}
          </Flexbox>
          <Text variant={"body-large"} useEllipsis={small}>
            {description}
          </Text>
          <Flexbox gap={"8px"} alignItems={"center"}>
            <Text variant={"title-small"}>RDS-code</Text>
            <Badge variant={"info"}>
              <Text variant={"body-medium"}>{rdsCode.toUpperCase()}</Text>
            </Badge>
          </Flexbox>
        </>
      )}
    </StyledDiv>
  );
};

const RdsSmallPreview = (rdsCode: string) => {
  const theme = useTheme();
  return (
    <Flexbox justifyContent={"center"} alignItems={"center"} flexDirection={"column"} gap={theme.tyle.spacing.base}>
      <RdsIcon color={theme.tyle.color.sys.pure.on} />
      <Text variant={"title-medium"} textAlign={"center"}>
        {rdsCode.toUpperCase()}
      </Text>
    </Flexbox>
  );
};
