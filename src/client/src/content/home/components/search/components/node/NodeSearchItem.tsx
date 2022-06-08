import { Duplicate, PencilAlt, Trash } from "@styled-icons/heroicons-outline";
import { TextResources } from "../../../../../../assets/text";
import textResources from "../../../../../../assets/text/TextResources";
import { Button } from "../../../../../../complib/buttons";
import { AlertDialog } from "../../../../../../complib/overlays/alert-dialog/AlertDialog";
import { useDeleteNode } from "../../../../../../data/queries/tyle/queriesNode";
import { PlainLink } from "../../../../../utils/PlainLink";
import { NodeItem } from "../../../../types/NodeItem";
import { Node } from "../../../about/components/node/Node";
import { NodePreview } from "../../../about/components/node/NodePreview";
import { Item } from "../item/Item";
import { ItemDescription } from "../item/ItemDescription";

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
      <PlainLink to={`/form/node/clone/${id}`}>
        <Button as={"span"} variant={"filled"} icon={<Duplicate />} iconOnly>
          {TextResources.ITEM_ACTION_CLONE}
        </Button>
      </PlainLink>
      <PlainLink to={`/form/node/edit/${id}`}>
        <Button as={"span"} variant={"filled"} icon={<PencilAlt />} iconOnly>
          {TextResources.ITEM_ACTION_EDIT}
        </Button>
      </PlainLink>
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
