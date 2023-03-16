import { NodeSearchItem, NodeSearchItemProps } from "features/explore/search/components/node/NodeSearchItem";
import { isNodeItem } from "features/explore/search/guards";
import { ConditionalSearchItem } from "features/explore/search/types/conditionalSearchItem";

type Props = ConditionalSearchItem & Pick<NodeSearchItemProps, "isSelected" | "setSelected">;

/**
 * Wrapper which simplifies rendering of a node given an unknown search item type.
 * Performs type check on the item property and renders if it matches the node type.
 *
 * @param item
 * @param isSelected
 * @param setSelected
 * @param user
 * @constructor
 */
export const ConditionalNodeSearchItem = ({ item, isSelected, setSelected, user }: Props) => {
  if (isNodeItem(item)) {
    return <NodeSearchItem key={item.id + item.kind} isSelected={isSelected} setSelected={setSelected} user={user} {...item} />;
  }

  return <></>;
};
