import {
  InterfaceSearchItem,
  InterfaceSearchItemProps,
} from "features/explore/search/components/interface/InterfaceSearchItem";
import { isInterfaceItem } from "features/explore/search/guards";
import { ConditionalSearchItem } from "features/explore/search/types/conditionalSearchItem";

type Props = ConditionalSearchItem & Pick<InterfaceSearchItemProps, "isSelected" | "setSelected">;

/**
 * Wrapper which simplifies rendering of an interface given an unknown search item type.
 * Performs type check on the item property and renders if it matches the interface type.
 *
 * @param item
 * @param isSelected
 * @param setSelected
 * @constructor
 */
export const ConditionalInterfaceSearchItem = ({ item, isSelected, setSelected }: Props) => {
  if (isInterfaceItem(item)) {
    return <InterfaceSearchItem key={item.id} isSelected={isSelected} setSelected={setSelected} {...item} />;
  }

  return <></>;
};
