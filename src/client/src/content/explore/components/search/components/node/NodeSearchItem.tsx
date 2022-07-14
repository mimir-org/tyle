import { Duplicate, PencilAlt, Trash } from "@styled-icons/heroicons-outline";
import { useTheme } from "styled-components";
import { TextResources } from "../../../../../../assets/text";
import textResources from "../../../../../../assets/text/TextResources";
import { Button } from "../../../../../../complib/buttons";
import { AlertDialog } from "../../../../../../complib/overlays/alert-dialog/AlertDialog";
import { useDeleteNode } from "../../../../../../data/queries/tyle/queriesNode";
import { NodePreview } from "../../../../../common/node";
import { PlainLink } from "../../../../../utils/PlainLink";
import { NodeItem } from "../../../../../types/NodeItem";
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
    preview={<NodePreview {...node} />}
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
  const theme = useTheme();
  const deleteNodeMutation = useDeleteNode();
  const deleteAction = {
    name: TextResources.ITEM_ACTION_DELETE,
    onAction: () => deleteNodeMutation.mutate(id),
    danger: true,
  };

  return (
    <>
      <PlainLink tabIndex={-1} to={`/form/node/clone/${id}`}>
        <Button tabIndex={0} as={"span"} icon={<Duplicate />} iconOnly>
          {TextResources.ITEM_ACTION_CLONE}
        </Button>
      </PlainLink>
      <PlainLink tabIndex={-1} to={`/form/node/edit/${id}`}>
        <Button tabIndex={0} as={"span"} icon={<PencilAlt />} iconOnly>
          {TextResources.ITEM_ACTION_EDIT}
        </Button>
      </PlainLink>
      <AlertDialog
        gap={theme.tyle.spacing.multiple(6)}
        actions={[deleteAction]}
        title={`${textResources.ITEM_ACTION_DELETE_TITLE} "${name}"?`}
        description={textResources.ITEM_ACTION_DELETE_DESCRIPTION}
        hideDescription
        content={<NodePreview name={name} color={color} img={img} terminals={terminals} />}
      >
        <Button icon={<Trash />} iconOnly>
          {TextResources.ITEM_ACTION_DELETE}
        </Button>
      </AlertDialog>
    </>
  );
};
