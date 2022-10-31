import {
  TransportSearchItem,
  TransportSearchItemProps,
} from "features/explore/search/components/transport/TransportSearchItem";
import { isTransportItem } from "features/explore/search/guards";
import { ConditionalSearchItem } from "features/explore/search/types/conditionalSearchItem";

type Props = ConditionalSearchItem & Pick<TransportSearchItemProps, "isSelected" | "setSelected">;

/**
 * Wrapper which simplifies rendering of a transport given an unknown search item type.
 * Performs type check on the item property and renders if it matches the transport type.
 *
 * @param item
 * @param isSelected
 * @param setSelected
 * @constructor
 */
export const ConditionalTransportSearchItem = ({ item, isSelected, setSelected }: Props) => {
  if (isTransportItem(item)) {
    return <TransportSearchItem key={item.id} isSelected={isSelected} setSelected={setSelected} {...item} />;
  }

  return <></>;
};
