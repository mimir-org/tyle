import {
  AspectObjectSearchItem,
  AspectObjectSearchItemProps,
} from "features/explore/search/components/aspectobject/AspectObjectSearchItem";
import { isAspectObjectItem } from "features/explore/search/guards";
import { ConditionalSearchItem } from "features/explore/search/types/conditionalSearchItem";

type Props = ConditionalSearchItem & Pick<AspectObjectSearchItemProps, "isSelected" | "setSelected">;

/**
 * Wrapper which simplifies rendering of a aspect object given an unknown search item type.
 * Performs type check on the item property and renders if it matches the aspect object type.
 *
 * @param item
 * @param isSelected
 * @param setSelected
 * @param user
 * @constructor
 */
export const ConditionalAspectObjectSearchItem = ({ item, isSelected, setSelected, user }: Props) => {
  if (isAspectObjectItem(item)) {
    return (
      <AspectObjectSearchItem
        key={item.id + item.kind}
        isSelected={isSelected}
        setSelected={setSelected}
        user={user}
        {...item}
      />
    );
  }

  return <></>;
};
