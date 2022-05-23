import { NodeItem } from "../../../../types/NodeItem";
import { useDeleteNode } from "../../../../../../data/queries/tyle/queriesNode";
import { Node } from "../../../about/components/node/Node";
import { ItemDescription } from "../item/ItemDescription";
import { Button } from "../../../../../../complib/buttons";
import { Duplicate, PencilAlt, Trash } from "@styled-icons/heroicons-outline";
import { TextResources } from "../../../../../../assets/text";
import { AlertDialog } from "../../../../../../complib/overlays/alert-dialog/AlertDialog";
import { NodePreview } from "../../../about/components/node/NodePreview";
import { Item } from "../item/Item";
import textResources from "../../../../../../assets/text/TextResources";

type NodeItemProps = NodeItem & {
  isSelected?: boolean;
  setSelected?: () => void;
};

/**
 * Component which visualizes a single node search-item with a preview, description and actions
 *
 * @param isSelected
 * @param setSelected
 * @param node
 * @constructor
 */
export const NodeSearchItem = ({ isSelected, setSelected, ...node }: NodeItemProps) => (
  <Item
    isSelected={isSelected}
    preview={<Node {...node} />}
    description={<ItemDescription onClick={setSelected} {...node} />}
    actions={<NodeSearchItemActions {...node} />}
  />
);

const NodeSearchItemActions = ({
  id,
  name,
  img,
  color,
  terminals,
}: Pick<NodeItem, "id" | "name" | "color" | "img" | "terminals">) => {
  const deleteNodeMutation = useDeleteNode();
  const deleteAction = {
    name: TextResources.ITEM_ACTION_DELETE,
    onAction: () => deleteNodeMutation.mutate(id),
    danger: true,
  };

  return (
    <>
      <Button disabled variant={"filled"} icon={<Duplicate />} iconOnly>
        {TextResources.ITEM_ACTION_CLONE}
      </Button>
      <Button disabled variant={"filled"} icon={<PencilAlt />} iconOnly>
        {TextResources.ITEM_ACTION_EDIT}
      </Button>
      <AlertDialog
        actions={[deleteAction]}
        title={`${textResources.ITEM_ACTION_DELETE_TITLE} "${name}"?`}
        description={textResources.ITEM_ACTION_DELETE_DESCRIPTION}
        content={<NodePreview color={color} img={img} terminals={terminals} />}
      >
        <Button variant={"outlined"} icon={<Trash />} iconOnly>
          {TextResources.ITEM_ACTION_DELETE}
        </Button>
      </AlertDialog>
    </>
  );
};
