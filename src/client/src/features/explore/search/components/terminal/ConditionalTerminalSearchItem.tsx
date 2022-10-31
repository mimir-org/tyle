import {
  TerminalSearchItem,
  TerminalSearchItemProps,
} from "features/explore/search/components/terminal/TerminalSearchItem";
import { isTerminalItem } from "features/explore/search/guards";
import { ConditionalSearchItem } from "features/explore/search/types/conditionalSearchItem";

type Props = ConditionalSearchItem & Pick<TerminalSearchItemProps, "isSelected" | "setSelected">;

/**
 * Wrapper which simplifies rendering of a terminal given an unknown search item type.
 * Performs type check on the item property and renders if it matches the terminal type.
 *
 * @param item
 * @param isSelected
 * @param setSelected
 * @constructor
 */
export const ConditionalTerminalSearchItem = ({ item, isSelected, setSelected }: Props) => {
  if (isTerminalItem(item)) {
    return <TerminalSearchItem key={item.id} isSelected={isSelected} setSelected={setSelected} {...item} />;
  }

  return <></>;
};
