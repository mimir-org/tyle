import { Text } from "../../../../../../complib/text";
import { ItemDescriptionContainer } from "./ItemDescription.styled";

interface Props {
  title: string;
  description: string;
  onClick?: () => void;
}

/**
 * Component which displays the title and description of a given item.
 *
 * @param title
 * @param description
 * @param onClick function that is triggered when clicking on the description itself
 * @constructor
 */
export const ItemDescription = ({ title, description, onClick }: Props) => {
  return (
    <ItemDescriptionContainer onClick={onClick}>
      <Text as={"span"} variant={"title-medium"}>
        {title}
      </Text>
      <Text as={"span"} useEllipsis ellipsisMaxLines={3}>
        {description}
      </Text>
    </ItemDescriptionContainer>
  );
};
