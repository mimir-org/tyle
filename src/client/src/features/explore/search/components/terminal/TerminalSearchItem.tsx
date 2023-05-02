import { State } from "@mimirorg/typelibrary-types";
import { DocumentDuplicate, PencilSquare, Trash, ChevronUp } from "@styled-icons/heroicons-outline";
import { TerminalItem } from "common/types/terminalItem";
import { UserItem } from "common/types/userItem";
import { Button } from "complib/buttons";
import { AlertDialog } from "complib/overlays";
import { usePatchTerminalState } from "external/sources/terminal/terminal.queries";
import { PlainLink } from "features/common/plain-link";
import { TerminalPreview } from "features/common/terminal/TerminalPreview";
import { Item } from "features/explore/search/components/item/Item";
import { ItemDescription } from "features/explore/search/components/item/ItemDescription";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { useButtonStateFilter } from "features/explore/search/hooks/useButtonFilter";

export type TerminalSearchItemProps = TerminalItem & {
  isSelected?: boolean;
  setSelected?: () => void;
  user: UserItem;
};

/**
 * Component which visualizes a terminal search-item with a preview, description and actions
 *
 * @param isSelected
 * @param setSelected
 * @param terminal
 * @param user
 * @constructor
 */
export const TerminalSearchItem = ({ isSelected, setSelected, user, ...terminal }: TerminalSearchItemProps) => (
  <Item
    isSelected={isSelected}
    preview={<TerminalPreview {...terminal} />}
    description={<ItemDescription onClick={setSelected} {...terminal} />}
    actions={<TerminalSearchItemActions user={user} terminal={terminal} />}
  />
);

type TerminalSearchItemActionProps = {
  user: UserItem;
  terminal?: TerminalItem;
};

const TerminalSearchItemActions = ({ user, terminal }: TerminalSearchItemActionProps) => {
  const theme = useTheme();
  const { t } = useTranslation("explore");
  const patcMutation = usePatchTerminalState();
  const btnFilter = useButtonStateFilter(terminal ?? null, user);

  if (user == null || terminal == null) return <></>;

  const deleteAction = {
    name: t("search.item.delete"),
    onAction: () => patcMutation.mutate({ id: terminal.id, state: State.Delete }),
  };

  const approveAction = {
    name: t("search.item.approve"),
    onAction: () => patcMutation.mutate({ id: terminal.id, state: State.Approve }),
  };

  const cloneLink = btnFilter.clone ? `/form/terminal/clone/${terminal.id}` : "#";
  const editLink = btnFilter.edit ? `/form/terminal/edit/${terminal.id}` : "#";

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
        title={t("search.item.templates.delete", { object: name })}
        description={t("search.item.deleteDescription")}
        hideDescription
        content={<TerminalPreview name={terminal.name} color={terminal.color} />}
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
        actions={[approveAction]}
        title={t("search.item.templates.approve")}
        description={t("search.item.approveDescription")}
        hideDescription
        content={<TerminalPreview name={terminal.name} color={terminal.color} />}
      >
        <Button
          disabled={!btnFilter.approve}
          variant={btnFilter.approved ? "outlined" : "filled"}
          icon={<ChevronUp />}
          iconOnly
        >
          {t("search.item.approve")}
        </Button>
      </AlertDialog>
    </>
  );
};
