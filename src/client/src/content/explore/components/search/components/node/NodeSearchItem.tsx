import { Duplicate, PencilAlt, Trash } from "@styled-icons/heroicons-outline";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Button } from "../../../../../../complib/buttons";
import { AlertDialog } from "../../../../../../complib/overlays/alert-dialog/AlertDialog";
import { useDeleteNode } from "../../../../../../data/queries/tyle/queriesNode";
import { NodePreview } from "../../../../../common/node";
import { NodeItem } from "../../../../../types/NodeItem";
import { PlainLink } from "../../../../../utils/PlainLink";
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
  const { t } = useTranslation("translation", { keyPrefix: "search.item" });

  const deleteNodeMutation = useDeleteNode();
  const deleteAction = {
    name: t("delete"),
    onAction: () => deleteNodeMutation.mutate(id),
  };

  return (
    <>
      <PlainLink tabIndex={-1} to={`/form/node/clone/${id}`}>
        <Button tabIndex={0} as={"span"} icon={<Duplicate />} iconOnly>
          {t("clone")}
        </Button>
      </PlainLink>
      <PlainLink tabIndex={-1} to={`/form/node/edit/${id}`}>
        <Button tabIndex={0} as={"span"} icon={<PencilAlt />} iconOnly>
          {t("edit")}
        </Button>
      </PlainLink>
      <AlertDialog
        gap={theme.tyle.spacing.multiple(6)}
        actions={[deleteAction]}
        title={t("templates.delete", { object: name })}
        description={t("deleteDescription")}
        hideDescription
        content={<NodePreview name={name} color={color} img={img} terminals={terminals} />}
      >
        <Button icon={<Trash />} iconOnly>
          {t("delete")}
        </Button>
      </AlertDialog>
    </>
  );
};
