import { Text } from "../../../../../complib/text";
import { ItemDescriptionContainer } from "./ItemDescription.styled";

export interface ItemDescriptionProps {
  name: string;
  description: string;
  onClick?: () => void;
}

/**
 * Component which displays the name and description of a given item inside a button.
 *
 * @param name
 * @param description
 * @param onClick function that is triggered when clicking on the description itself
 * @constructor
 */
export const ItemDescription = ({ name, description, onClick }: ItemDescriptionProps) => {
  return (
    <ItemDescriptionContainer onClick={onClick}>
      <Text as={"span"} variant={"title-medium"} useEllipsis ellipsisMaxLines={1}>
        {name}
      </Text>
      <Text as={"span"} useEllipsis ellipsisMaxLines={3}>
        {description}
      </Text>
    </ItemDescriptionContainer>
  );
};
