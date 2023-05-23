import styled from "styled-components/macro";
import { Text } from "../../../../complib/text";
import Badge from "../../../ui/badges/Badge";

interface RdsPreviewProps {
  name: string;
  description: string;
  rdsCode: string;
  small?: boolean;
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
  background-color: ${(props) => props.theme.tyle.color.sys.surface.base};
  border: 1px solid ${(props) => props.theme.tyle.color.sys.outline.base};
  width: ${(props) => (props.small ? "200px" : "100%")};
`;

export const RdsPreview = ({ name, description, rdsCode, small }: RdsPreviewProps): JSX.Element => {
  return (
    <StyledDiv small={small}>
      <Text variant={small ? "title-small" : "display-small"} useEllipsis={small}>
        {name}
      </Text>
      <Badge variant={"info"}>
        <Text variant={small ? "body-small" : "body-large"}>{rdsCode}</Text>
      </Badge>
      <Text variant={small ? "body-small" : "body-large"} useEllipsis={small}>
        {description}
      </Text>
    </StyledDiv>
  );
};
