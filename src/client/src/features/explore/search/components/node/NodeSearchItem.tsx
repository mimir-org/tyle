import { State } from "@mimirorg/typelibrary-types";
import { DocumentDuplicate, PencilSquare, Trash, ChevronUp, ChevronDoubleUp } from "@styled-icons/heroicons-outline";
import { NodeItem } from "common/types/nodeItem";
import { UserItem } from "common/types/userItem";
import { Button } from "complib/buttons";
import { AlertDialog } from "complib/overlays";
import { usePatchNodeState } from "external/sources/node/node.queries";
import { NodePreview } from "features/common/node";
import { PlainLink } from "features/common/plain-link";
import { Item } from "features/explore/search/components/item/Item";
import { ItemDescription } from "features/explore/search/components/item/ItemDescription";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { useButtonStateFilter } from "features/explore/search/hooks/useButtonFilter";

export type NodeSearchItemProps = NodeItem & {
  isSelected?: boolean;
  setSelected?: () => void;
  user: UserItem;
};

/**
 * Component which visualizes a single node search-item with a preview, description and actions
 *
 * @param isSelected
 * @param setSelected
 * @param node
 * @param user
 * @constructor
 */
export const NodeSearchItem = ({ isSelected, setSelected, user, ...node }: NodeSearchItemProps) => (
  <Item
    isSelected={isSelected}
    preview={<NodePreview {...node} />}
    description={<ItemDescription onClick={setSelected} {...node} />}
    actions={<NodeSearchItemActions user={user} node={node} />}
  />
);

type NodeSearchItemActionProps = {
  user: UserItem;
  node?: NodeItem;
};

const NodeSearchItemActions = ({ user, node }: NodeSearchItemActionProps) => {
  const theme = useTheme();
  const { t } = useTranslation("explore");
  const patcMutation = usePatchNodeState();
  const btnFilter = useButtonStateFilter(node ?? null, user);

  if (user == null || node == null) return <></>;

  const deleteAction = {
    name: t("search.item.delete"),
    onAction: () => patcMutation.mutate({ id: node.id, state: State.Delete }),
  };

  const approveCompanyAction = {
    name: t("search.item.approve"),
    onAction: () => patcMutation.mutate({ id: node.id, state: State.ApproveCompany }),
  };

  const approveGlobalAction = {
    name: t("search.item.approve"),
    onAction: () => patcMutation.mutate({ id: node.id, state: State.ApproveGlobal }),
  };

  const cloneLink = btnFilter.clone ? `/form/node/clone/${node.id}` : "#";
  const editLink = btnFilter.edit ? `/form/node/edit/${node.id}` : "#";

  return (
    <>
      <PlainLink tabIndex={-1} to={cloneLink}>
        <Button
          disabled={!btnFilter.clone}
          tabIndex={0}
          as={!btnFilter.clone ? "button" : "span"}
          icon={<DocumentDuplicate />}
          iconOnly
        >
          {t("search.item.clone")}
        </Button>
      </PlainLink>
      <PlainLink tabIndex={-1} to={editLink}>
        <Button
          disabled={!btnFilter.edit}
          tabIndex={0}
          as={!btnFilter.edit ? "button" : "span"}
          icon={<PencilSquare />}
          iconOnly
        >
          {t("search.item.edit")}
        </Button>
      </PlainLink>
      <AlertDialog
        gap={theme.tyle.spacing.multiple(6)}
        actions={[deleteAction]}
        title={t("search.item.templates.delete")}
        description={t("search.item.deleteDescription")}
        hideDescription
        content={<NodePreview name={node.name} color={node.color} img={node.img} terminals={node.terminals} />}
      >
        <Button
          disabled={!btnFilter.delete}
          variant={btnFilter.deleted ? "outlined" : "filled"}
          icon={<Trash />}
          iconOnly
        >
          {t("search.item.delete")}
        </Button>
      </AlertDialog>

      <AlertDialog
        gap={theme.tyle.spacing.multiple(6)}
        actions={[approveCompanyAction]}
        title={t("search.item.templates.approveCompany")}
        description={t("search.item.approveDescription")}
        hideDescription
        content={<NodePreview name={node.name} color={node.color} img={node.img} terminals={node.terminals} />}
      >
        <Button
          disabled={!btnFilter.approveCompany}
          variant={btnFilter.approvedComapny ? "outlined" : "filled"}
          icon={<ChevronUp />}
          iconOnly
        >
          {t("search.item.approve")}
        </Button>
      </AlertDialog>

      <AlertDialog
        gap={theme.tyle.spacing.multiple(6)}
        actions={[approveGlobalAction]}
        title={t("search.item.templates.approveGlobal")}
        description={t("search.item.approveDescription")}
        hideDescription
        content={<NodePreview name={node.name} color={node.color} img={node.img} terminals={node.terminals} />}
      >
        <Button
          disabled={!btnFilter.approveGlobal}
          variant={btnFilter.approvedGlobal ? "outlined" : "filled"}
          icon={<ChevronDoubleUp />}
          iconOnly
        >
          {t("search.item.approve")}
        </Button>
      </AlertDialog>
    </>
  );
};
