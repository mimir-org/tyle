import { State } from "@mimirorg/typelibrary-types";
import { Duplicate, PencilAlt, Trash } from "@styled-icons/heroicons-outline";
import { NodeItem } from "common/types/nodeItem";
import { Button } from "complib/buttons";
import { AlertDialog } from "complib/overlays";
import { usePatchNodeState } from "external/sources/node/node.queries";
import { NodePreview } from "features/common/node";
import { PlainLink } from "features/common/plain-link";
import { Item } from "features/explore/search/components/item/Item";
import { ItemDescription } from "features/explore/search/components/item/ItemDescription";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

export type NodeSearchItemProps = NodeItem & {
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
export const NodeSearchItem = ({ isSelected, setSelected, ...node }: NodeSearchItemProps) => (
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
  const { t } = useTranslation("explore");

  const deleteNodeMutation = usePatchNodeState();
  const deleteAction = {
    name: t("search.item.delete"),
    onAction: () => deleteNodeMutation.mutate({ id, state: State.Delete }),
  };

  return (
    <>
      <PlainLink tabIndex={-1} to={`/form/node/clone/${id}`}>
        <Button tabIndex={0} as={"span"} icon={<Duplicate />} iconOnly>
          {t("search.item.clone")}
        </Button>
      </PlainLink>
      <PlainLink tabIndex={-1} to={`/form/node/edit/${id}`}>
        <Button tabIndex={0} as={"span"} icon={<PencilAlt />} iconOnly>
          {t("search.item.edit")}
        </Button>
      </PlainLink>
      <AlertDialog
        gap={theme.tyle.spacing.multiple(6)}
        actions={[deleteAction]}
        title={t("search.item.templates.delete", { object: name })}
        description={t("search.item.deleteDescription")}
        hideDescription
        content={<NodePreview name={name} color={color} img={img} terminals={terminals} />}
      >
        <Button icon={<Trash />} iconOnly>
          {t("search.item.delete")}
        </Button>
      </AlertDialog>
    </>
  );
};
