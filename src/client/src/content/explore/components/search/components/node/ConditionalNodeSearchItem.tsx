import { isNodeItem } from "../../../../../../utils/guards";
import { ConditionalSearchItem } from "../../../../types/conditionalSearchItem";
import { NodeSearchItem, NodeSearchItemProps } from "./NodeSearchItem";

type Props = ConditionalSearchItem & Pick<NodeSearchItemProps, "isSelected" | "setSelected">;

/**
 * Wrapper which simplifies rendering of a node given an unknown search item type.
 * Performs type check on the item property and renders if it matches the node type.
 *
 * @param item
 * @param isSelected
 * @param setSelected
 * @constructor
 */
export const ConditionalNodeSearchItem = ({ item, isSelected, setSelected }: Props) => {
  if (isNodeItem(item)) {
    return <NodeSearchItem key={item.id} isSelected={isSelected} setSelected={setSelected} {...item} />;
  }

  return <></>;
};
